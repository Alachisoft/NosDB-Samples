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
using System.Configuration;
using NosDB.Client;
using NosDB.Common;
using NosDB.Samples.EntityObjects;
using System.Collections.Generic;
using NosDB.Client.Async;
using NosDB.Common.Server.Engine;

namespace NosDB.Samples
{
    /// <summary>
    /// Class that provides the functionality of the sample
    /// </summary>
    public class ClrTriggersUsage
    {
        private Database _database;
        private DBCollection<Product> _productsCollection;

        /// <summary>
        /// Executing this method will perform all the operations of the sample
        /// </summary>
        public void Run()
        {
            // Generate first product
            Product product1 = GenerateProduct1();

            // Generate second product
            Product product2 = GenerateProduct2();

            // Initialize database and collection for further use
            InitalizeDatabaseAndCollection();

            // Insert documents in the collection
            InsertDocuments(product1, product2);

            // Update documents in the collection
            UpdateDocuments(product1);

            // Delete documents from the collection
            DeleteDocuments(product1);

            //Releasing the resources
            _database.Dispose();
        }

        /// <summary>
        /// This method initializes the database and collection using the connection string provided
        /// in the app.config
        /// </summary>
        private void InitalizeDatabaseAndCollection()
        {
            string conectionString = ConfigurationManager.AppSettings["ConnectionString"];
			string collection = ConfigurationManager.AppSettings["CollectionName"];
			
            // Initialize an instance of the database to begin performing operations:
            _database = NosDB.Client.NosDBClient.GetDatabase(conectionString);

            // Get an instance of the 'Products' collection from the database:
            _productsCollection = _database.GetDBCollection<Product>(collection);
        }

        /// <summary>
        /// This method inserts products in the collection
        /// </summary>
        /// <param name="product1"> First product that is to be inserted in the collection </param>
        /// <param name="product2"> Second product that is to be inserted in the collection </param>
        private void InsertDocuments(Product product1, Product product2)
        {
            ICollection<Product> products = new List<Product>();
            products.Add(product1);
            products.Add(product2);

            // Insert documents into the Products collection:
            List<FailedDocument> failedDocuments = _productsCollection.InsertDocuments(products, WriteConcern.InMemory);

            // Printing Failed Documents on Insert Information
            foreach (FailedDocument failedDocument in failedDocuments)
            {
                PrintFailedDocumentDetails(failedDocument);
            }
        }

        /// <summary>
        /// This method updates (replaces) the documents in the collection
        /// </summary>
        /// <param name="product1"> First product that will be modified and updated in the collection </param>
        private void UpdateDocuments(Product product1)
        {
            // Replace products with one product's unit price empty
            product1.UnitPrice = 0;
            ICollection<Product> replacableProducts = new List<Product>();
            replacableProducts.Add(product1);

            // Replace documents into the Products collection:
            List<FailedDocument> failedDocuments = _productsCollection.UpdateDocuments(replacableProducts, WriteConcern.InMemory);

            foreach (FailedDocument failedDocument in failedDocuments)
            {
                PrintFailedDocumentDetails(failedDocument);
            }
        }

        /// <summary>
        /// This method deletes the products from the collection
        /// </summary>
        /// <param name="product1"> First product whos ID will be used to delete data from the collection </param>
        private void DeleteDocuments(Product product1)
        {
            ICollection<DocumentKey> productKeys = new List<DocumentKey>();
            productKeys.Add(product1.ID);

            _productsCollection.DeleteDocuments(productKeys);
        }

        /// <summary>
        /// This method generates an instance of the <see cref="Product"/> class
        /// </summary>
        /// <returns> Returns a new instance of <see cref="Product"/></returns>
        private Product GenerateProduct1()
        {
            Product product1 =
              new Product
              {
                  ID = 1001,
                  ProductName = "Blue Marble Jack Cheese",
                  UnitPrice = 400,
                  Category = new EmbeddedCategory() { CategoryID = 11, CategoryName = "Milk Products" }
              };
            return product1;
        }

        /// <summary>
        /// This method generates an instance of the <see cref="Product"/> class
        /// </summary>
        /// <returns> Returns a new instance of <see cref="Product"/></returns>
        private Product GenerateProduct2()
        {
            Product product2 = new Product
            {
                ID = 1002,
                ProductName = "Peanut Biscuits",
                Category = new EmbeddedCategory() { CategoryID = 12, CategoryName = "Snacks" }
            };
            return product2;
        }

        /// <summary>
        /// A method for printing all the failed documents and the reason for their failure.
        /// </summary>
        /// <param name="failedDocument"></param>
        private static void PrintFailedDocumentDetails(FailedDocument failedDocument)
        {
            if (failedDocument == null)
                return;

            Console.WriteLine("Document Key : " + failedDocument.DocumentKey);
            Console.WriteLine("Error Code : " + failedDocument.ErrorCode);
            Console.WriteLine("Error Message : " + failedDocument.ErrorMessage + "\n");
        }

    }
}
