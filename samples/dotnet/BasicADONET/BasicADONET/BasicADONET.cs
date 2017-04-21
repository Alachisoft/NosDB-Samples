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
using System.Data;
using System.Data.Common;
using NosDB.Samples.EntityObjects;

namespace NosDB.Samples
{
    /// <summary>
    /// Class that provides the functionality of the sample
    /// </summary>
    public class BasicADONET
    {
        private DbConnection _connection;
        private DbProviderFactory _factory;

        /// <summary>
        /// Executing this method will perform all the operations of the sample
        /// </summary>
        public void Run()
        {

            // Generate new product that will be used in this sample)
            Product product = GenerateProduct(4534073843);

            // Initialize database and collection
            InitializeDatabase();

            // Insert product in the collection
            InsertQuery(product);

            // Fetch product from the collection
            GetQuery(product);

            // Update product into the collection
            UpdateQuery(product);

            // Delete product from the collection
            DeleteQuery(product);

            // Releasing all the resources
            _connection.Dispose();
        }

        /// <summary>
        /// This method initializes the database and the collection
        /// </summary>
        private void InitializeDatabase()
        {
            // ----------------------------------------------------------------------
            // NosDB database connection through ADO.NET

            // Read "NosDbConnection" from "connectionStrings" section in App.Config 
            ConnectionStringSettings connectionSettings = ConfigurationManager.ConnectionStrings["NosDbConnection"];

            // Load the NosDB ADO.NET Provider and use it to open connection to the database
            _factory = DbProviderFactories.GetFactory(connectionSettings.ProviderName);
            _connection = _factory.CreateConnection();

            _connection.ConnectionString = connectionSettings.ConnectionString;
            _connection.Open();
        }

        /// <summary>
        /// This method inserts the given product in the collection
        /// </summary>
        /// <param name="product"> Product that is intended to be inserted </param>
        private void InsertQuery(Product product)
        {
            Console.WriteLine("INSERT a Product into northwind database...");

            string insertStmt = "INSERT INTO Products (ProductID, ProductName, UnitPrice, UnitsInStock) ";
            insertStmt += "VALUES (" + product.ID + ", '" + product.ProductName + "'";
            insertStmt += ", " + product.UnitPrice + ", " + product.UnitsInStock + ")";

            // Create an ADO.NET Command object & set its connection property
            DbCommand command = _factory.CreateCommand();
            command.CommandText = insertStmt;
            command.Connection = _connection;

            // Execute the INSERT statement and get "number of rows affected" value
            int rowsAffected = command.ExecuteNonQuery();

            Console.WriteLine(rowsAffected + " Product(s) inserted successfully");
        }

        /// <summary>
        /// This method fetches data from the collection
        /// </summary>
        /// <param name="product"> Product whos ID will be used to fetch data from the collection </param>
        private void GetQuery(Product product)
        {
            Console.WriteLine("SELECT Products from northwind database...");

            string sqlStmt = "SELECT ProductID, ProductName, UnitPrice, UnitsInStock FROM Products WHERE ProductID = " + product.ID;

            // Create an ADO.NET Command object & set its connection property
            DbCommand command = _factory.CreateCommand();
            command.CommandText = sqlStmt;
            command.Connection = _connection;

            // Execute the SELECT statement and get a "reader" containing query result.
            IDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Product p = new Product();
                p.ID = (long)reader["ProductID"];
                p.ProductName = (string)reader["ProductName"];
                p.UnitPrice = (double)reader["UnitPrice"];
                p.UnitsInStock = (int)((long)reader["UnitsInStock"]);

                Console.WriteLine("ProductID: " + p.ID + "ProductName: " + p.ProductName + " UnitPrice: " + p.UnitPrice +
                                 " UnitsInStock: " + p.UnitsInStock);
            }

            Console.WriteLine();
        }

        /// <summary>
        /// This method updates the product in the collection
        /// </summary>
        /// <param name="product"> product that will be modified in the collection </param>
        private void UpdateQuery(Product product)
        {
            product.ProductName = "Test Product UPDATED";
            product.UnitPrice = 127.75;
            product.UnitsInStock = 200;

            Console.WriteLine();
            Console.WriteLine("UPDATE a Product to northwind database...");


            string updateStmt = "UPDATE Products SET Name = '" + product.ProductName + "'";
            updateStmt += ", UnitPrice = " + product.UnitPrice;
            updateStmt += ", UnitsInStock = " + product.UnitsInStock;
            updateStmt += " WHERE ProductID = " + product.ID;

            // Create an ADO.NET Command object & set its connection property
            DbCommand command = _factory.CreateCommand();
            command.CommandText = updateStmt;
            command.Connection = _connection;

            // Execute the UPDATE statement and get "number of rows affected" value
            int rowsAffected = command.ExecuteNonQuery();

            Console.WriteLine(rowsAffected + " Product(s) updated successfully");
        }

        /// <summary>
        /// This method deletes the specified product from the collection
        /// </summary>
        /// <param name="product"> Product whos ID will be used to delete data from collection </param>
        private void DeleteQuery(Product product)
        {
            Console.WriteLine("DELETE a Product from northwind database...");
            string deleteStmt = "DELETE FROM Products WHERE ProductID = " + product.ID;

            // Create an ADO.NET Command object & set its connection property
            DbCommand command = _factory.CreateCommand();
            command.CommandText = deleteStmt;
            command.Connection = _connection;

            // Execute the DELETE statement and get "number of rows affected" value
            int rowsAffected = command.ExecuteNonQuery();

            Console.WriteLine(rowsAffected + " Product(s) deleted successfully");
        }

        /// <summary>
        /// This method generates a new instance of <see cref="Product"/> class
        /// </summary>
        /// <param name="productId"> The product generated will have this product ID as its key </param>
        /// <returns> Returns a new instance of <see cref="Product"/></returns>
        private Product GenerateProduct(long productId)
        {
            Product prod = new Product();
            prod.ID = productId;
            prod.ProductName = "Test Product";
            prod.UnitPrice = 107.75;
            prod.UnitsInStock = 100;
            return prod;
        }
    }
}
