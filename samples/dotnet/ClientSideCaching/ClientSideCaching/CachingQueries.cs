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
using NosDB.Common.Caching;
using NosDB.Common.Server.Engine;
using NosDB.Common.Server.Engine.Impl;
using NosDB.Samples.EntityObjects;

namespace NosDB.Samples
{
    /// <summary>
    /// Class that provides the functionality of the sample
    /// </summary>
    public class CachingQueries
    {
        private Database _database;
        private DBCollection<Product> _productsCollection;

        /// <summary>
        /// Executing this method will perform all the operations of the sample
        /// </summary>
        public void Run()
        {
            // Generate new product
            Product product = GenerateProduct(1001);

            // Initialize database and collection for further use
            InitializeDatabaseAndCollection();

            // Insert document in the collection/cache
            InsertDocument(product);

            // Fetch document from the collection/cache
            GetDocument(product);

            // Update document in the collection/cache
            UpdateDocument(product);

            // Delete document from the collection/cache
            DeleteDocument(product);

            // Query data from the collection
            QueryWithCaching();

            // Releasing the occupied resources
            _database.Dispose();
        }

        /// <summary>
        /// This method initializes database and collection for futher use and sets the caching behvior
        /// to cache all data
        /// </summary>
        private void InitializeDatabaseAndCollection()
        {
            // Initialize an instance of the database to begin performing operations:
            string connectionString = ConfigurationManager.AppSettings["ConnectionString"];
			string collection = ConfigurationManager.AppSettings["CollectionName"];
			
            _database = Client.NosDBClient.GetDatabase(connectionString);

            // Get an instance of the 'Products' collection from the database:
            _productsCollection = _database.GetDBCollection<Product>(collection);

            // Cache behavior for this collection can be set using following property. By default, behavior specified in config file is used. However, specifying in code will override the settings specified in app.config.
            _productsCollection.CachingBehavior = CachingBehavior.CacheAll;
        }

        /// <summary>
        /// This method inserts product in the collection. The data is also cached
        /// </summary>
        /// <param name="product"> Product that will be inserted </param>
        private void InsertDocument(Product product)
        {
            // Insert into the Products collection. If caching is enabled, document will automatically be inserted into cache.
            _productsCollection.InsertDocument(product, WriteConcern.InMemory);

            Console.WriteLine("Product Inserted in to database.");
        }

        /// <summary>
        /// This method fetches data from the cache if found otherwise it fetches from the database and
        /// then it inserts it in the cache
        /// </summary>
        /// <param name="product"> Product whos ID will be used to fetch data </param>
        private void GetDocument(Product product)
        {
            // Get from the Products collection. Document will be fetched from cache if exists. If not, document will be inserted into cache when fetched from database.
            Product fetchedProduct = _productsCollection.GetDocument(product.ID, readPreference: ReadPreference.PrimaryOnly);

            Console.WriteLine("Product fetched from the database.");
            // If you want to skip cache and fetch data from database, call this method this way.
            // Product fetchedProduct = productsCollection.GetDocument("1", readPreference: ReadPreference.PrimaryOnly, useCache:false);
        }

        /// <summary>
        /// This method updates product in the collection. It will also remove document from the cache
        /// </summary>
        /// <param name="product"> Product that will be modified and relaced in the collection </param>
        private void UpdateDocument(Product product)
        {
            // Update the UnitsInStock for the product which need to be replaced.
            product.UnitsInStock += 100;

            // Replace in the Products collection. This call will also removed document from cache.
            _productsCollection.UpdateDocument(product, WriteConcern.InMemory);

            Console.WriteLine("Product updated in the database.");
        }

        /// <summary>
        /// This method deletes document from the collection. It will also remove the document from the cache
        /// </summary>
        /// <param name="product"> Product whos ID will be used to remove document from the collection 
        /// and cache </param>
        private void DeleteDocument(Product product)
        {
            // Delete from the Products collection. This call will also remove documents from cache.
            _productsCollection.DeleteDocument(product.ID, WriteConcern.InMemory);

            Console.WriteLine("Product deleted from the database. ");
        }

        /// <summary>
        /// This method queries data from the collection and also store it in cache
        /// </summary>
        private void QueryWithCaching()
        {
            // Get the 'Chai' product from 'Products' Collection using Query Caching
            const string selectQueryWithParameters = "SELECT * FROM Products WHERE ProductName = @productName";
            IList<IParameter> selectParameters = new List<IParameter>();
            selectParameters.Add(new Parameter("productName", "Chai"));

            // Executing Select Query:
            // The 'CacheOptions.Cache' passed as the fourth argument signals that the result be cached.
            // We pass a TimeSpan of 15 minutes in the arguments. This sets the expiration time for caching the query.
            // Once the expiration time passes, these items will be removed from the cache.
            IDBCollectionReader reader = _database.ExecuteReader(selectQueryWithParameters, selectParameters,
                ReadPreference.PrimaryOnly,
                CacheOptions.Cache,
                ExpirationType.AbsoluteExpiration, new TimeSpan(0, 0, 15, 0));

            // Similarly ExpirationType.SlidingExpiration can also be Passed in ExecuteReader Method instead of ExpirationType.AbsoluteExpiration
            int totalDocs = 0;
            while (reader.ReadNext())
            {
                // Get the entire product from the Reader
                Product product = reader.GetObject<Product>();
                totalDocs++;
            }

            Console.WriteLine(totalDocs + " Products retrieved from the database. ");
        }

        /// <summary>
        /// This method generates a new instance of <see cref="Product"/> class
        /// </summary>
        /// <param name="productId"> The generated instance will have this id </param>
        /// <returns> Returns a new instance of <see cref="Product"/></returns>
        private Product GenerateProduct(long productId)
        {
            Product product = new Product
            {
                ID = productId,
                ProductName = "Blue Marble Jack Cheese",
                UnitPrice = 400,
                Category = new EmbeddedCategory() { CategoryID = 11, CategoryName = "Milk Products" }
            };
            return product;
        }
    }
}