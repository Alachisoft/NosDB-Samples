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
using NosDB.Samples.EntityObjects;

namespace NosDB.Samples
{
    /// <summary>
    /// Class that provides the functionality of the sample
    /// </summary>
    class BasicDocumentApi
    {
        private DBCollection<Product> _productsCollection;
        Database _database;

        /// <summary>
        /// Executing this method will perform all the operations of the sample
        /// </summary>
        public void Run()
        {
            // Generate products for sample application
            ICollection<Product> products = GenerateProducts();

            // Initialize databasea and collection
            InitializeDatabaseAndCollection();

            // Insert multiple documents into the collection
            InsertMultipleDocuments(products);

            // Fetch multiple documents from the collection
            GetMultipleDocuments(products);

            // Update multiple documents into the collection
            UpdateMultipleDocuments(products);

            // Delete multiple documents from the collection
            DeleteMultipleDocuments(products);

            // Releasing the resources
            _database.Dispose();
        }

        /// <summary>
        /// Initalizes the database using the connection string provided in app.config and
        /// gets products collection
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
            // Initialize an instance of the database to begin performing operations
            _database = Client.NosDBClient.GetDatabase(connectionString);

            // Get an instance of the collection from the database
            _productsCollection = _database.GetDBCollection<Product>(collection);
        }

        /// <summary>
        /// This method inserts multple product entries in the products collection
        /// </summary>
        /// <param name="products"> Products that will be inserted in the collection </param>
        private void InsertMultipleDocuments(ICollection<Product> products)
        {
            // Insert documents into the Products collection
            // With WriteConcern.InMemory
            // This API returns the documents which could not be inserted.
            List<FailedDocument> failedDocsOnInsert = _productsCollection.InsertDocuments(products, WriteConcern.InMemory);

            if (failedDocsOnInsert == null || failedDocsOnInsert.Count == 0)
                Console.WriteLine("All items inserted into the '{0}' collection successfully. \n", _productsCollection.Name);
            else
            {
                Console.WriteLine("One or more items could not be inserted to the '{0}' collection.\n", _productsCollection.Name);

                foreach (var failedDocument in failedDocsOnInsert)
                {
                    PrintFailedDocumentDetails(failedDocument);
                }
            }
        }

        /// <summary>
        /// This method fetches products from the products collection
        /// </summary>
        /// <param name="products"> products whos ID will be used to fetch data from the collection </param>
        private void GetMultipleDocuments(ICollection<Product> products)
        {
            ICollection<DocumentKey> productKeys = GetProductKeys(products);

            // Get documents from the Products collection:
            // With ReadPreference.PrimaryOnly
            // It will return a reader populated with the retrieved items. You may iterate the reader as demonstrated below.
            IDBCollectionReader reader = _productsCollection.GetDocuments(productKeys);
            Console.WriteLine("\n\nThe following items were retrieved from the Products collection:\n");
            while (reader.ReadNext())
            {
                Product fetchedProduct = reader.GetObject<Product>();
                PrintProductDetails(fetchedProduct);
                Console.WriteLine("\n");
            }
            reader.Dispose();
        }

        /// <summary>
        /// This method updates (replaces) the products in the products collection
        /// </summary>
        /// <param name="products"> Products that will be modified and replaced in the collection </param>
        private void UpdateMultipleDocuments(ICollection<Product> products)
        {
            // Update the UnitsInStock for the documents which need to be replaced.
            IEnumerator<Product> ie = products.GetEnumerator();
            while (ie.MoveNext())
            {
                ie.Current.UnitsInStock += 100;
            }
            // Update documents in the Products collection
            // With WriteConcern.InMemory
            // This API returns the documents which could not be replaced.
            List<FailedDocument> failedDocsOnUpdate = _productsCollection.UpdateDocuments(products, WriteConcern.InMemory);

            if (failedDocsOnUpdate == null || failedDocsOnUpdate.Count == 0)
                Console.WriteLine("All items updated into the '{0}' collection successfully. \n", _productsCollection.Name);
            else
            {
                Console.WriteLine("One or more items could not be updated to the '{0}' collection.\n", _productsCollection.Name);

                foreach (var failedDocument in failedDocsOnUpdate)
                {
                    PrintFailedDocumentDetails(failedDocument);
                }
            }
        }

        /// <summary>
        /// This method deletes the specified products from the product collection
        /// </summary>
        /// <param name="products"> products whos ID will be used to delete data from the collection </param>
        private void DeleteMultipleDocuments(ICollection<Product> products)
        {
            ICollection<DocumentKey> productKeys = GetProductKeys(products);

            // Delete documents from the Products collection:
            // With WriteConcern.InMemory
            // This API returns the documents which could not be deleted.
            List<FailedDocument> failedDocsOnDelete = _productsCollection.DeleteDocuments(productKeys, WriteConcern.InMemory);

            if (failedDocsOnDelete == null || failedDocsOnDelete.Count == 0)
                Console.WriteLine("All items deleted from the '{0}' collection successfully.\n ", _productsCollection.Name);
            else
            {
                Console.WriteLine("One or more items could not be deleted from the '{0}' collection.\n", _productsCollection.Name);

                foreach (var failedDocument in failedDocsOnDelete)
                {
                    PrintFailedDocumentDetails(failedDocument);
                }
            }
        }

        /// <summary>
        /// Generates products that will be used in different operations during this sample
        /// </summary>
        /// <returns> Returns a list of products </returns>
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
        /// A method for printing the details of the product type.
        /// </summary>
        /// <param name="product"> Product whos information will be printed </param>
        private static void PrintProductDetails(Product product)
        {
            if (product == null)
                return;
            Console.WriteLine("ProductID : " + product.ID);
            Console.WriteLine("ProductName : " + product.ProductName);
            Console.WriteLine("UnitPrice : " + product.UnitPrice);
        }

        /// <summary>
        /// A method for printing all the failed documents and the reason for their failure.
        /// </summary>
        /// <param name="failedDocument"> Failed document whos information will be printed </param>
        private static void PrintFailedDocumentDetails(FailedDocument failedDocument)
        {
            if (failedDocument == null)
                return;

            Console.WriteLine("Document Key : " + failedDocument.DocumentKey);
            Console.WriteLine("Error Code : " + failedDocument.ErrorCode);
            Console.WriteLine("Error Message : " + failedDocument.ErrorMessage);
        }
    }
}
