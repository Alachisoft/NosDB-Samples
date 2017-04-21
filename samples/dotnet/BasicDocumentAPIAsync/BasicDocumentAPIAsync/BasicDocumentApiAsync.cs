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
using NosDB.Client.Async;
using NosDB.Common;
using NosDB.Common.Server.Engine;
using NosDB.Samples.EntityObjects;

namespace NosDB.Samples
{
    /// <summary>
    /// Class that provides the functionality of the sample
    /// </summary>
    public class BasicDocumentApiAsync
    {
        private Database _database;
        private DBCollection<Product> _productsCollection;

        /// <summary>
        /// Executing this method will perform all the operations of the sample
        /// </summary>
        public void Run()
        {
            // Generate multiple products that will be used in different operation in this sample
            ICollection<Product> products = GenerateProducts();

            // Initialize database and collection for the operations
            InitializeDatabaseAndCollection();

            // Insert multiple documents asynchronously in the collection
            InsertMultipleDocumentsAsync(products);

            // Update mulctiple documents asynchronously in the collection
            UpdateMultipleDocumentsAsync(products);

            // Delte the docments added in the collection asynchronously
            DeleteMultipleDocumentsAsync(products);

            //Releasing the resources
            _database.Dispose();
        }

        /// <summary>
        /// This method initializes the database and the product collection
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
            // Initialize an instance of the database to begin performing operations:
            _database = Client.NosDBClient.GetDatabase(connectionString);

            // Get an instance of the 'Products' collection from the database:
            _productsCollection = _database.GetDBCollection<Product>(collection);
        }

        /// <summary>
        /// This method inserts multiple documents in the collection using async api
        /// </summary>
        /// <param name="products"> products that will be inserted in the collection </param>
        private void InsertMultipleDocumentsAsync(ICollection<Product> products)
        {
            // Insert documents into the Products collection:
            // Pass the callback in the arguments.
            _productsCollection.InsertDocumentsAsync(products, AsyncOperationCallBack, null, WriteConcern.InMemory);

            // Wait for the callback.
            Thread.Sleep(1000);
        }

        /// <summary>
        /// Update multiple documents in the collection using async api
        /// </summary>
        /// <param name="products"> Products that will be modified and updated in the collection </param>
        private void UpdateMultipleDocumentsAsync(ICollection<Product> products)
        {
            // Update the UnitsInStock for the documents which need to be replaced.
            IEnumerator<Product> ie = products.GetEnumerator();
            while (ie.MoveNext())
            {
                ie.Current.UnitsInStock += 100;
            }

            // Replace documents in the Products collection:
            // Pass the callback in the arguments.
            _productsCollection.UpdateDocumentsAsync(products, AsyncOperationCallBack, null, WriteConcern.InMemory);

            // Wait for the callback.
            Thread.Sleep(1000);
        }

        /// <summary>
        /// Delete multiple dcouments from the collection using async api
        /// </summary>
        /// <param name="products"> Products whos ID will be used to delte documents from the collection </param>
        private void DeleteMultipleDocumentsAsync(ICollection<Product> products)
        {
            // Get product IDs to be deleted
            ICollection<DocumentKey> productKeys = GetProductKeys(products);

            // Delete documents from the Products collection:
            // Pass the callback in the arguments.
            _productsCollection.DeleteDocumentsAsync(productKeys, AsyncOperationCallBack, WriteConcern.InMemory);

            // Wait for the callback.
            Thread.Sleep(1000);
        }

        /// <summary>
        /// Generates list of products to be used in this sample
        /// </summary>
        /// <returns> returns list of Products </returns>
        private ICollection<Product> GenerateProducts()
        {
            ICollection<Product> products = new List<Product>();
            products.Add(new Product { ID = 1001, ProductName = "Blue Marble Jack Cheese", UnitPrice = 400, Category = new EmbeddedCategory() { CategoryID = 11, CategoryName = "Milk Products" } });
            products.Add(new Product { ID = 1002, ProductName = "Peanut Biscuits", UnitPrice = 500, Category = new EmbeddedCategory() { CategoryID = 12, CategoryName = "Snacks" } });
            products.Add(new Product { ID = 1003, ProductName = "Walmart Delicious Cream", UnitPrice = 300, Category = new EmbeddedCategory() { CategoryID = 11, CategoryName = "Milk Products" } });
            products.Add(new Product { ID = 1004, ProductName = "Nestle Yogurt", UnitPrice = 400, Category = new EmbeddedCategory() { CategoryID = 11, CategoryName = "Milk Products" } });
            products.Add(new Product { ID = 1005, ProductName = "American Butter", UnitPrice = 600, Category = new EmbeddedCategory() { CategoryID = 11, CategoryName = "Milk Products" } });
            return products;
        }
		
		        /// <summary>
        /// Generates product keys from a collection of products
        /// </summary>
        /// <param name="products"> Products whos keys are to be returned </param>
        /// <returns> List of product keys </returns>
        private ICollection<DocumentKey> GetProductKeys(ICollection<Product> products)
        {
            ICollection<DocumentKey> productKeys = new List<DocumentKey>();
            foreach (Product product in products)
            {
                productKeys.Add(product.ID);
            }
            return productKeys;
        } 

        /// <summary>
        /// The callback method which will be executed each time an async operation completes.
        /// </summary>
        /// <param name="eventArgs"> Async operation event arguments </param>
        private static void AsyncOperationCallBack(AsyncEventArgs eventArgs)
        {
            if (eventArgs != null)
            {
                Console.WriteLine("Callback for the " + eventArgs.AsyncOperationType + " operation received.");
                if (eventArgs.Success)
                    Console.WriteLine("The operation was completed successfully.\n");
                else
                {
                    Console.WriteLine("The operation was completed with errors.\n");
                    if (eventArgs.FailedDocuments != null)
                    {
                        foreach (var failedDocument in eventArgs.FailedDocuments)
                        {
                            PrintFailedDocumentDetails(failedDocument);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// A method for printing all the failed documents and the reason for their failure.
        /// </summary>
        /// <param name="failedDocument"> Failed document whos information is to be printed </param>
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
