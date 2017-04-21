// ===============================================================================
// Alachisoft (R) NosDB Sample Code.
// ===============================================================================
// Copyright © Alachisoft.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using NosDB.Client;
using NosDB.Common.Exceptions;
using NosDB.Common.Server.Engine;
using NosDB.Common.Server.Engine.Impl;
using NosDB.Common.SqlNotifications;
using NosDB.Samples.EntityObjects;

namespace NosDB.Samples
{
    /// <summary>
    /// Class that provides the functionality of the sample
    /// </summary>
    public class SqlDependency
    {
        private Database _database;
        private DBCollection<Product> _productsCollection;

        /// <summary>
        /// Executing this method will perform all the operations of the sample
        /// </summary>
        public void Run()
        {
            // Generate new product that will be used during the sample
            Product cheeseProduct = GenerateProduct();

            // Initalize database and collection
            InitializeDatabaseAndCollection();

            // Insert document in the collection to register sql dependency
            InsertDocument(cheeseProduct);

            // Register sql dependency
            RegisterSqlDependency(cheeseProduct);

            // Perform update on the document to triger sql dependency
            UpdateDocument(cheeseProduct);

            // Wait for the update notification.
            Thread.Sleep(1000);

            // Delete the inserted document
            DeleteDocument(cheeseProduct);

            // Releasing all the resources
            _database.Dispose();
        }

        /// <summary>
        /// This Event will be fired whenever sql dependency is triggered
        /// </summary>
        /// <param name="source"> The source of the event </param>
        /// <param name="changeEventArg"> Event arguments </param>
        private void OnDependencyChange(object source, Common.Events.DependencyChangeEventArg changeEventArg)
        {
            Console.WriteLine("Event Type: " + changeEventArg.NotificationInfo);
            Console.WriteLine("Source Type: " + changeEventArg.Source + "\n");
        }

        /// <summary>
        /// This method initalizes database and collection using the connection string specified in app.config
        /// </summary>
        private void InitializeDatabaseAndCollection()
        {
            string connectionString = ConfigurationManager.AppSettings["ConnectionString"];
			string collection = ConfigurationManager.AppSettings["CollectionName"];

            if (String.IsNullOrEmpty(connectionString))
            {
                Console.WriteLine("The connection string cannot be null or empty.");
                return;
            }
            //Initialize an instance of the database to begin performing operations:
            _database = Client.NosDBClient.GetDatabase(connectionString);

            //Get an instance of the collection from the database:
            _productsCollection = _database.GetDBCollection<Product>(collection);
        }

        /// <summary>
        /// This method inserts the product in the collection
        /// </summary>
        /// <param name="cheeseProduct"> Product that is to be inserted in the collection </param>
        private void InsertDocument(Product cheeseProduct)
        {
            // Insert the 'Blue Marble Jack Cheese' document into the 'Products' collection:
            // This API returns an OperationFailedException if the document is already present in the collection.
            try
            {
                _productsCollection.InsertDocument(cheeseProduct);
                Console.WriteLine("Product with ProductID '" + cheeseProduct.ID + "' inserted into database");
            }
            catch (OperationFailedException e)
            {
                Console.WriteLine(e.Message + "\n");
            }
        }

        /// <summary>
        /// This method registers an sql dependency 
        /// </summary>
        private void RegisterSqlDependency(Product cheeseProduct)
        {
            // Initialize a new instance of the NosDB.Client.SqlDependency class using the 
            // connection string and register it to receive the notifications:
            Client.SqlDependency sqlDependency = new Client.SqlDependency(ConfigurationManager.AppSettings["ConnectionString"]);

            QueryCommand cmd = new QueryCommand();
            cmd.CommandText = "SELECT * FROM Products WHERE ProductName = @productName";
            cmd.Parameters = new List<IParameter>();
            cmd.Parameters.Add(new Parameter("productName", cheeseProduct.ProductName));
            sqlDependency.DependencyChange += OnDependencyChange;
            sqlDependency.Register(cmd);
            Console.WriteLine("Dependency registered for product having name 'Blue Marble Jack Cheese'");
        }

        /// <summary>
        /// This method updates the unitsInStock of the specified product in the collection
        /// </summary>
        /// <param name="cheeseProduct"> Product that will be updated in the collection </param>
        private void UpdateDocument(Product cheeseProduct)
        {
            // To receive the notification, we need to perform an operation on the 'Blue MArble Jack Cheese' product.
            // Here, we update the product's UnitsInStock:
            string updateQueryString = "UPDATE Products SET (UnitsInStock = @unitsInStock) WHERE ProductName =  @productName";

            IList<IParameter> updateParameters = new List<IParameter>();
            updateParameters.Add(new Parameter("unitsInStock", 1000));
            updateParameters.Add(new Parameter("productName", cheeseProduct.ProductName));

            Console.WriteLine("Updating unitsInStock for product 'Blue Marble Jack Cheese'");
            long rowsAffected = _database.ExecuteNonQuery(updateQueryString, updateParameters);

            Console.WriteLine("Rows affected due to the update query : " + rowsAffected + "\n");
        }

        /// <summary>
        /// This method deletes product form the collection
        /// </summary>
        /// <param name="cheeseProduct"> Product whos ID will be used to delete data from the collection </param>
        private void DeleteDocument(Product cheeseProduct)
        {
            // Delete the document that was inserted.
            _productsCollection.DeleteDocument(cheeseProduct.ID);
        }

        /// <summary>
        /// This method generates a new instance of <see cref="Product"/> class
        /// </summary>
        /// <returns> Instance that will be returned </returns>
        private Product GenerateProduct()
        {
            var product = new Product
            {
                ID = 1001,
                ProductName = "Blue Marble Jack Cheese",
                UnitPrice = 400,
                Category = new EmbeddedCategory()
                {
                    CategoryID = 11,
                    CategoryName = "Milk Products"
                }
            };
            return product;
        }

    }
}
