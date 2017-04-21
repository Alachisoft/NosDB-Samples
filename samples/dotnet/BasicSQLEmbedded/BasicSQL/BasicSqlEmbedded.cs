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
using NosDB.Common;
using NosDB.Common.Server.Engine;
using NosDB.Common.Server.Engine.Impl;
using NosDB.Samples.EntityObjects;

namespace NosDB.Samples
{
    /// <summary>
    /// Class that provides the functionality of the sample
    /// </summary>
    public class BasicSqlEmbedded
    {
        private Database _database;

        /// <summary>
        /// Executing this method will perform all the operations of the sample
        /// </summary>
        public void Run()
        {
            // Generate product to be used in this sample
            Product product = GenerateProduct(1001);

            // Generate category to be embedded in product
            EmbeddedCategory category = GenerateCategory(13);

            // Initialize database 
            InitializeDatabase();

            // Insert document using query
            InsertDocumentsUsingQuery(product, category);

            // Get document using query
            GetDocumentsUsingQuery(product);

            // Update document using query
            UpdateDocumentsUsingQuery(product);

            // Delete document using query
            DeleteDocumentsUsingQuery(product);

            //Releasing the resources
            _database.Dispose();
        }

        /// <summary>
        /// This method initializes the database using the connection string provided in app.Conf
        /// </summary>
        private void InitializeDatabase()
        {
            string conectionString = ConfigurationManager.AppSettings["ConnectionString"];

            //Initialize an instance of the database to begin performing operations:
            _database = Client.NosDBClient.GetDatabase(conectionString);
        }

        /// <summary>
        /// This method inserts a product in the collection
        /// </summary>
        /// <param name="product"> Product that is to be inserted in the collection </param>
        /// <param name="category"> Category that will be embedded in the products </param>
        private void InsertDocumentsUsingQuery(Product product, EmbeddedCategory category)
        {
            // Insert Product 'Chai' into the 'Products' Collection using Insert Query.
            const string insertQuery =
                "INSERT INTO Products (ProductID, ProductName, UnitsInStock, UnitPrice, Category) VALUES(@productId, @productName, @unitsInStock, @unitPrice, @category)";

            IList<IParameter> insertParameters = new List<IParameter>();
            insertParameters.Add(new Parameter("productId", product.ID));
            insertParameters.Add(new Parameter("productName", product.ProductName));
            insertParameters.Add(new Parameter("unitsInStock", product.UnitsInStock));
            insertParameters.Add(new Parameter("unitPrice", product.UnitPrice));
            // Category is an embedded document here
            insertParameters.Add(new Parameter("category", category));

            // Executing Insert Query
            long affectedRows = _database.ExecuteNonQuery(insertQuery, insertParameters);

            Console.WriteLine("'" + affectedRows + "' document(s) were inserted");
        }

        /// <summary>
        /// This method fetches documents from the database using query
        /// </summary>
        /// <param name="product"> Product whos ID will be used to fetch data from the database </param>
        private void GetDocumentsUsingQuery(Product product)
        {
            // Get the product 'Chai' from the 'Products' Collection using the Select Query
            const string selectQuery =
                "SELECT ProductName, UnitsInStock, UnitPrice, Category FROM Products WHERE ProductID = @productID";
            IList<IParameter> selectParameters = new List<IParameter>();
            selectParameters.Add(new Parameter("productID", product.ID));

            // Executing Select Query
            IDBCollectionReader reader = _database.ExecuteReader(selectQuery, selectParameters);
            int count = 0;
            while (reader.ReadNext())
            {
                // Get String Attribute from Reader                   
                string productName = reader.GetString("ProductName");

                // Get short Attribute from Reader
                short unitsInStock = reader.GetInt16("UnitsInStock");

                // Get double Attribute from Reader
                double unitprice = reader.GetDouble("UnitPrice");

                EmbeddedCategory category = reader.Get<EmbeddedCategory>("Category");

                // Get Complete Product form Reader
                Product fetchedProduct = reader.GetObject<Product>();
                count++;
            }
            reader.Dispose();

            Console.WriteLine("'" + count + "' document(s) were retrieved");
        }

        /// <summary>
        /// This method updates document using query
        /// </summary>
        /// <param name="product"> Product that will be modified and inserted in the collection </param>
        private void UpdateDocumentsUsingQuery(Product product)
        {
            // Update item in the 'Products' Collection using Update Query. Notice how we're updating the Name attribute inside the Category. 
            const string updateQuery =
                "UPDATE Products SET (UnitsInStock = @unitsInStock, UnitPrice = @unitPrice, Category.CategoryName = @categoryName) WHERE ProductID = @productID";
            IList<IParameter> updateParametes = new List<IParameter>();
            updateParametes.Add(new Parameter("unitsInStock", 45));
            updateParametes.Add(new Parameter("unitPrice", 125.0));
            updateParametes.Add(new Parameter("productID", product.ID));
            updateParametes.Add(new Parameter("categoryName", "Food"));

            // Executing Update Query
            long affectedRows = _database.ExecuteNonQuery(updateQuery, updateParametes);

            Console.WriteLine("'" + affectedRows + "' document(s) were updated");
        }

        /// <summary>
        /// This method deletes data from the collection using query
        /// </summary>
        /// <param name="product"> Product whos ID will be used to delete data from the collection </param>
        private void DeleteDocumentsUsingQuery(Product product)
        {
            // Delete product from the Products Collection using Delete Query.
            const string deleteQuery = "DELETE FROM Products WHERE ProductID = @productID";

            IList<IParameter> deleteParametes = new List<IParameter>();
            deleteParametes.Add(new Parameter("productID", product.ID));

            long deletedDocuments = _database.ExecuteNonQuery(deleteQuery, deleteParametes);

            Console.WriteLine("'" + deletedDocuments + "' document(s) were deleted");
        }

        /// <summary>
        /// This method generates a new <see cref="Product"/> instance with the specified ID
        /// </summary>
        /// <param name="productId"> Product generated will have this product ID </param>
        /// <returns></returns>
        private Product GenerateProduct(long productId)
        {
            var product = new Product();
            product.ID = productId;
            product.ProductName = "Chai";
            product.UnitsInStock = 39;
            product.UnitPrice = 100.0;
            return product;
        }

        /// <summary>
        /// This method generates a new <see cref="EmbeddedCategory"/> instance with the specified ID
        /// </summary>
        /// <param name="categoryId"> Category generated will have this category ID </param>
        /// <returns></returns>
        private EmbeddedCategory GenerateCategory(long categoryId)
        {
            var category = new EmbeddedCategory();
            category.CategoryID = categoryId;
            category.CategoryName = "Beverages";
            return category;
        }
    }
}
