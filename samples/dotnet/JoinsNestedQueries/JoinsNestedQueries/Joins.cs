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
using NosDB.Samples.EntityObjects;

namespace NosDB.Samples
{
    /// <summary>
    /// Class that provides the functionality of the sample
    /// </summary>
    public class Joins
    {
        private Database _database;

        /// <summary>
        /// Executing this method will perform all the operations of the sample
        /// </summary>
        public void Run()
        {
            // Initialize database 
            InitializeDatabase();

            // Perform inner join
            PerformInnerJoin();

            // Perform left outer join
            PerformLeftOuterJoin();

            // perform right outer join
            PerformRightOuterJoin();

            // Perform self join
            PerformSelfJoin();

            // Execute sub query
            PerformSubQuery();

            //Releasing the resources
            _database.Dispose();
        }

        /// <summary>
        /// This method initializes the database using the connection string specified in app.config
        /// </summary>
        private void InitializeDatabase()
        {
            string connectionString = ConfigurationManager.AppSettings["ConnectionString"];

            if (String.IsNullOrEmpty(connectionString))
            {
                Console.WriteLine("The connection string cannot be null or empty.");
                return;
            }
            // Initialize an instance of the database to begin performing operations:
            _database = Client.NosDBClient.GetDatabase(connectionString);
        }

        /// <summary>
        /// This method executes inner join on orders and employees collections
        /// </summary>
        private void PerformInnerJoin()
        {
            var resultset = new List<Tuple<Order, Employee>>();
            Console.WriteLine("Following query joins Orders and Employees Data whose shipment city is London and Freight greater than 100lb.\n");

            string query = "SELECT * FROM Orders o JOIN Employees e ON o.EmployeeID = e.EmployeeID " +
                           "WHERE o.ShipCity = 'London' AND o.Freight > 100 ORDER BY o.OrderID ";

            Console.WriteLine(query);
            IDBCollectionReader reader = _database.ExecuteReader(query);
            while (reader.ReadNext())
            {
                var order = new Order()
                {
                    ID = reader.GetInt64("OrderID"),
                    OrderDate = reader.Get<DateTime>("OrderDate"),
                    EmployeeID = reader.GetInt64("EmployeeID"),
                    CustomerID = reader.GetString("CustomerID"),
                };

                var employee = new Employee()
                {
                    EmployeeID = reader.GetInt64("e_EmployeeID"),
                    FirstName = reader.GetString("e_FirstName"),
                    LastName = reader.GetString("e_LastName"),
                    Title = reader.GetString("e_Title"),
                };
                resultset.Add(new Tuple<Order, Employee>(order, employee));
            }

            Console.WriteLine(string.Format("Inner Join returned {0} documents.", resultset.Count + "\n\n\n"));
            resultset.Clear();
            reader.Dispose();
        }

        /// <summary>
        /// This method executes left outer join on orders and employees collections
        /// </summary>
        private void PerformLeftOuterJoin()
        {
            Console.WriteLine("Following query joins Orders and Employees Data using Left Outer Join whose shipment city is Paris.\n");

            var resultset = new List<Tuple<Order, Employee>>();
            string query = "SELECT o.OrderID, o.ShipCity, e.FirstName, e.City " +
                    "FROM Orders o LEFT JOIN Employees e ON o.EmployeeID = e.EmployeeID " +
                     "WHERE o.ShipCity = 'Paris'";

            Console.WriteLine(query);
            IDBCollectionReader reader = _database.ExecuteReader(query);
            while (reader.ReadNext())
            {
                var order = new Order()
                {
                    ID = reader.GetInt64("OrderID"),
                    ShipCity = reader.GetString("ShipCity"),
                };

                var employee = new Employee()
                {
                    FirstName = reader.ContainsAttribute("FirstName") ? reader.GetString("FirstName") : null,
                    City = reader.ContainsAttribute("City") ? reader.GetString("City") : null,
                };
                resultset.Add(new Tuple<Order, Employee>(order, employee));
            }

            Console.WriteLine(string.Format("Left Outer Join returned {0} documents.", resultset.Count+ "\n\n\n"));
            resultset.Clear();
            reader.Dispose();
        }

        /// <summary>
        /// This method executes right outer join on orders and employees collections
        /// </summary>
        private void PerformRightOuterJoin()
        {
            Console.WriteLine("Following query joins Orders and Employees Data using Right Outer Join for those Employees whose Title is Sales Representative. \n");

            var resultset = new List<Tuple<Order, Employee>>();
            string query = "SELECT o.OrderID, o.ShipCity, e.FirstName, e.City " +
                    "FROM Orders o RIGHT JOIN Employees e ON o.EmployeeID = e.EmployeeID " +
                     "WHERE e.Title= 'Sales Representative'";

            Console.WriteLine(query);
            IDBCollectionReader reader = _database.ExecuteReader(query);
            while (reader.ReadNext())
            {
                var order = new Order()
                {
                    ID = reader.ContainsAttribute("OrderID") ? reader.GetInt64("OrderID") : -1,
                    ShipCity = reader.ContainsAttribute("ShipCity") ? reader.GetString("ShipCity") : null,
                };

                var employee = new Employee()
                {
                    FirstName = reader.GetString("FirstName"),
                    City = reader.GetString("City"),
                };
                resultset.Add(new Tuple<Order, Employee>(order, employee));
            }

            Console.WriteLine(string.Format("Right Outer Join returned {0} documents.", resultset.Count+ "\n\n\n"));
            resultset.Clear();
            reader.Dispose();
        }

        /// <summary>
        /// This method executes self join on employees collection
        /// </summary>
        private void PerformSelfJoin()
        {
            Console.WriteLine("Following query correlates Employees with Manager/Supervisors using Self Join.\n");

            string query = "SELECT * FROM Employees emp JOIN Employees empMgr ON emp.ReportsTo = empMgr.EmployeeID ";

            Console.WriteLine(query);
            var employeManager = new List<Tuple<Employee, Employee>>();
            IDBCollectionReader reader = _database.ExecuteReader(query);
            while (reader.ReadNext())
            {
                var employee = new Employee()
                {
                    EmployeeID = reader.GetInt64("EmployeeID"),
                    FirstName = reader.GetString("FirstName"),
                    LastName = reader.GetString("LastName"),
                    Title = reader.GetString("Title"),
                };
                var manager = new Employee()
                {
                    EmployeeID = reader.GetInt64("empMgr_EmployeeID"),
                    FirstName = reader.GetString("empMgr_FirstName"),
                    LastName = reader.GetString("empMgr_LastName"),
                    Title = reader.GetString("empMgr_Title"),
                };
                employeManager.Add(new Tuple<Employee, Employee>(employee, manager));
            }
            Console.WriteLine(string.Format("Self Join returned {0} documents.", employeManager.Count + "\n\n\n"));
            employeManager.Clear();
            reader.Dispose();
        }

        /// <summary>
        /// This method executes sub query on customer collection along with the parent query on orders collection
        /// </summary>
        private void PerformSubQuery()
        {
            Console.WriteLine("Following subquery fetches Customers living in London and Order wrights more than 100lb.\n");

            string query = "SELECT * FROM Customers c  WHERE c.CustomerID IN (" +
                          "SELECT o.CustomerID FROM Orders o WHERE o.Freight > 100) " +
                    "AND c.City = 'London'";

            Console.WriteLine(query);
            var customers = new List<Customer>();
            IDBCollectionReader reader = _database.ExecuteReader(query);
            while (reader.ReadNext())
            {
                var customer = new Customer()
                {
                    ID = reader.GetString("CustomerID"),
                    ContactName = reader.GetString("ContactName"),
                    ContactTitle = reader.GetString("ContactTitle"),
                    Phone = reader.GetString("Phone"),
                };
                customers.Add(customer);
            }
            Console.WriteLine(string.Format("Subquery returned {0} documents.", customers.Count + "\n\n\n"));
            customers.Clear();
            reader.Dispose();
        }
    }
}
