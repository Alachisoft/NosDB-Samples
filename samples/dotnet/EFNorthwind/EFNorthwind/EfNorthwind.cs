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
using System.Linq;
using NosDB.Common;
using NosDB.Samples.NorthwindModels;
using Microsoft.EntityFrameworkCore;

namespace NosDB.Samples
{
    /// <summary>
    /// Sample class that shows the usage of EF with NosDB
    /// </summary>
    public class EfNorthwind
    {
        NorthwindContext _northwindDB;

        /// <summary>
        /// Executing this method will perform inserts, updates, deletes and queries on NosDB using Entity framework
        /// </summary>
        public void Run()
        {
            // Get instance of database, this databaseContext instance is used to do all the operations
            using (_northwindDB = new NorthwindContext())
            {
                // Generate list of products to be used by this sample
                ICollection<Products> products = GenereateProducts();

                // Insert multiple documents using Entity framework's API
                InsertMultipleDocuments(products);

                // Get multiple documents from NosDB using different queries on Entity framework
                GetMultipleDocuments();

                // Update multiple documents in NosDB using Entity framework's update API
                UpdateMultipleDocuments(products);

                // Delete the documents that were inserted by this sample using Entity framework's delete API
                DeleteMultipleDocuments(products);
            }
        }

        /// <summary>
        /// This method inserts products in the collection
        /// </summary>
        /// <param name="products"> Products that will be inserted </param>
        private void InsertMultipleDocuments(ICollection<Products> products)
        {
            _northwindDB.Products.AddRange(products);
            int insertsPerformed = _northwindDB.SaveChanges();

            Console.WriteLine("Products inserted into the database.");
            Console.WriteLine();
        }

        /// <summary>
        /// This method fetches multiple documents using different types of queries
        /// </summary>
        private void GetMultipleDocuments()
        {
            //Simple Select Query with criteria (Lambda Expression)
            var simpleSelectQuery = _northwindDB.Products.Where(product => product.ProductId == 1001).Select(product => product);
            var simpleSelectResult = simpleSelectQuery.ToList();
            Console.WriteLine("Items Retrieved = " + simpleSelectResult.Count);

            //Embedded Query (Query Expression)
            var embeddedSelectQuery = (from items in _northwindDB.Orders
                                       where items.OrderId > 1000
                                       select items).Include(items => items.OrderDetails);
            var embeddedSelectResult = embeddedSelectQuery.ToList();
            Console.WriteLine("Items Retrieved = " + embeddedSelectResult.Count);

            //Normalized Query (Query Expression)
            var normalizedSelectQuery = from customer in _northwindDB.Customers
                                        join order in _northwindDB.Orders
                                        on customer.CustomerId equals order.CustomerId
                                        select new { customer, order };
            var normalizedResult = normalizedSelectQuery.ToList();
            Console.WriteLine("Items Retrieved = " + normalizedResult.Count);

            //Aggregate Count query (Lambda Expression)
            string customerID = "ALFKI";
            int orderCount = _northwindDB.Orders.Where(order => order.CustomerId == customerID).Select(order => order).Count();

            Console.WriteLine("Count of Orders placed by CustomerID " + customerID + " = " + orderCount);
            Console.WriteLine();
        }

        /// <summary>
        /// This method updates multiple documents
        /// </summary>
        /// <param name="products"> Products that will be modified and updated in the collection </param>
        private void UpdateMultipleDocuments(ICollection<Products> products)
        {
            IEnumerator<Products> ie = products.GetEnumerator();
            while (ie.MoveNext())
            {
                ie.Current.UnitsInStock += 100;
            }
            _northwindDB.Products.UpdateRange(products);

            int updatesPerformed = _northwindDB.SaveChanges();

            Console.WriteLine("Products updated in the database.");
            Console.WriteLine();
        }

        /// <summary>
        /// This method deletes the documents from the collection
        /// </summary>
        /// <param name="products"> Products that will be removed from the collection </param>
        private void DeleteMultipleDocuments(ICollection<Products> products)
        {
            _northwindDB.Products.RemoveRange(products);
            int deletesPerformed = _northwindDB.SaveChanges();

            Console.WriteLine("Products deleted from the database.");
            Console.WriteLine();
        }

        /// <summary>
        /// This method generates a list of <see cref="Products"/>
        /// </summary>
        /// <returns> Returns a list of <see cref="Products"/></returns>
        private ICollection<Products> GenereateProducts()
        {
            ICollection<Products> products = new List<Products>();
            products.Add(new Products { ProductId = 1001, ProductName = "Blue Marble Jack Cheese", UnitPrice = 400, Category = new Category() { CategoryId = 11, CategoryName = "Milk Products", ProductId = 1001 } });
            products.Add(new Products { ProductId = 1002, ProductName = "Peanut Biscuits", UnitPrice = 500, Category = new Category() { CategoryId = 12, CategoryName = "Snacks", ProductId = 1002 } });
            products.Add(new Products { ProductId = 1003, ProductName = "Walmart Delicious Cream", UnitPrice = 300, Category = new Category() { CategoryId = 11, CategoryName = "Milk Products", ProductId = 1003 } });
            products.Add(new Products { ProductId = 1004, ProductName = "Nestle Yogurt", UnitPrice = 400, Category = new Category() { CategoryId = 11, CategoryName = "Milk Products", ProductId = 1004 } });
            products.Add(new Products { ProductId = 1005, ProductName = "American Butter", UnitPrice = 600, Category = new Category() { CategoryId = 11, CategoryName = "Milk Products", ProductId = 1005 } });
            return products;
        }
    }
}
