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
using NosDB.Client;
using NosDB.Client.Linq;
using NosDB.Common.Server.Engine;
using NosDB.Samples.EntityObjects;
using System.Linq;

namespace NosDB.Samples
{
    /// <summary>
    /// Class that provides the functionality of the sample
    /// </summary>
    public class BasicLINQ
    {
        private Database _database;
        private DBCollection<Product> _productsCollection;
        private DBUpdatable<Product> _prodUpdatable;
        private IQueryable<Product> _prodQueryable;

        /// <summary>
        /// Executing this method will perform all the operations of the sample
        /// </summary>
        public void Run()
        {
            // Generate product1
            Product product1 = GenerateProduct1();

            // Generate product2
            Product prodcut2 = GenerateProduct2();

            // Initialize Database and collections
            InitializeDatabaseAndCollection();

            // Get IQueryable instance of collections
            GetIQueryableInstances();

            // Insert multiple documents using linq
            InsertUsingExtendedLinq(product1, prodcut2);

            // Get documents using linq
            GetUsingLinq();

            // Update document using linq
            UpdateUsingExtendedLinq(product1);

            // Delete document using linq
            DeleteUsingExtendedLinq(product1, prodcut2);

            // Releasing the resources
            _database.Dispose();
        }

        /// <summary>
        /// This method initailizes database and collection
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

            //Get an instance of the 'Products' collection from the database:
            _productsCollection = _database.GetDBCollection<Product>(collection);
        }

        /// <summary>
        /// This method gets the IQueryable instances of collection to be used by linq
        /// </summary>
        private void GetIQueryableInstances()
        {
            // Get Extended IQueryable instance to run LINQ write queries
            _prodUpdatable = _productsCollection.AsUpdateable();
            // Get IQueryable instance to run LINQ read queries
            _prodQueryable = _productsCollection.AsQueryable();
        }

        /// <summary>
        /// This method inserts multiple products in the collection using linq extended operators
        /// </summary>
        /// <param name="product1"> First product that will be inserted </param>
        /// <param name="product2"> Second prdouct that will be inserted </param>
        private void InsertUsingExtendedLinq(Product product1, Product product2)
        {
            // Insert document into the Products collection:
            _prodUpdatable.Insert(product1, WriteConcern.InMemory);
            _prodUpdatable.Insert(product2, WriteConcern.InMemory);

            Console.WriteLine("Inserted 2 documents successfully!");
        }

        /// <summary>
        /// This method fetches documents from the collection using linq
        /// </summary>
        private void GetUsingLinq()
        {
            List<Product> products = _prodQueryable.Where(x => x.UnitPrice > 300)
                .OrderBy(u => u.ProductName)
                .Select(
                    x =>
                        new Product
                        {
                            ID = x.ID,
                            ProductName = x.ProductName,
                            UnitsInStock = x.UnitsInStock,
                            UnitPrice = x.UnitPrice
                        }).ToList();


            Console.WriteLine(string.Format("{0} documents fetched from '{1}' collection", products.Count,
                _productsCollection.Name));

            Console.WriteLine("The following products were fetched:\n");
            foreach (var product in products)
            {
                Console.WriteLine(product.ID + " " + product.ProductName + " " + product.UnitsInStock + " " +
                                  product.UnitPrice);
            }
        }

        /// <summary>
        /// This method updates documents in the collection using linq extended operators
        /// </summary>
        /// <param name="product1"> Product that will be modified during the operation </param>
        private void UpdateUsingExtendedLinq(Product product1)
        {
            // Update the UnitsInStock, UnitPrice for the products with category "Milk Products"
            DBUpdateBuilder update = DBUpdate.Set("UnitsInStock", 3).Set("UnitPrice", 420);
            // Update documents in the Products collection:
            long rowsAffected = _prodUpdatable.Update(update, x => x.ID == product1.ID, WriteConcern.InMemory);
            Console.WriteLine(string.Format("{0} documents updated in '{1}' collection", rowsAffected,
                _productsCollection.Name));
        }

        /// <summary>
        /// This method deletes documents from collection using linq extended operators
        /// </summary>
        /// <param name="product1"> Product whos ID will be used to delete documents </param>
        /// <param name="product2"> Product whos ID will be used to delete documents </param>
        private void DeleteUsingExtendedLinq(Product product1, Product product2)
        {
            // Delete products with category "Dairy Products" and UnitPrice > 300
            long rowsAffected = _prodUpdatable.Delete(x => x.UnitsInStock < 10 && x.UnitPrice > 300 && x.ID == product1.ID || x.ID == product2.ID, WriteConcern.InMemory);
            Console.WriteLine(string.Format("{0} documents deleted in '{1}' collection", rowsAffected,
                _productsCollection.Name));
        }

        /// <summary>
        /// This method generates a sample prodcut to be used by this sample
        /// </summary>
        /// <returns> Returns a new instance of product </returns>
        private Product GenerateProduct1()
        {
            Product prod1 = new Product
            {
                ID = 1001,
                ProductName = "Blue Marble Jack Cheese",
                UnitPrice = 400,
                UnitsInStock = 1
            };
            return prod1;

        }

        /// <summary>
        /// This method generates a sample prodcut to be used by this sample
        /// </summary>
        /// <returns> Returns a new instance of product </returns>
        private Product GenerateProduct2()
        {
            Product prod2 = new Product
            {
                ID = 1002,
                ProductName = "Peanut Biscuits",
                UnitPrice = 500,
                UnitsInStock = 2
            };
            return prod2;
        }
    }
}