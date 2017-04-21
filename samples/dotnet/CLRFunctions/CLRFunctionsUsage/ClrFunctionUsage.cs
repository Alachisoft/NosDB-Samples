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

namespace NosDB.Samples
{
    /// <summary>
    /// Class that provides the functionality of the sample
    /// </summary>
    public class ClrFunctionUsage
    {
        private Database _database;

        /// <summary>
        /// Executing this method will perform all the operations of the sample
        /// </summary>
        public void Run()
        {
            // Initialize database for further use
            InitializeDatabase();

            // Query data that uses Clr function
            QueryWithClrFunction();

            // Query data that uses Clr function provider
            QueryWithClrFunctionProvider();

            // Releasing the resources
            _database.Dispose();
        }

        /// <summary>
        /// This method initializes the database using the connection string provided in the app.config
        /// </summary>
        private void InitializeDatabase()
        {
            string conectionString = ConfigurationManager.AppSettings["ConnectionString"];

            //Initialize an instance of the database to begin performing operations:
            _database = Client.NosDBClient.GetDatabase(conectionString);
        }

        /// <summary>
        /// This method queries data from the collection. The query uses the clr function
        /// </summary>
        private void QueryWithClrFunction()
        {
            const string selectQuery = "SELECT CustomerID, OrderDetails[0].ProductID, OrderDetails[0].UnitPrice, OrderDetails[0].Quantity, OrderDetails[0].Discount,  CalculateAmount(OrderDetails[0].UnitPrice, OrderDetails[0].Quantity, OrderDetails[0].Discount) as CalculatedAmount FROM Orders WHERE CalculateAmount(OrderDetails[0].UnitPrice, OrderDetails[0].Quantity, OrderDetails[0].Discount) < 12.0";

            //Executing Select Query
            IDBCollectionReader reader = _database.ExecuteReader(selectQuery);
            int count = 0;
            while (reader.ReadNext())
            {
                //Get String Attribute from Reader
                string customerId = reader.GetString("CustomerID");
                long productId = reader.GetInt64("ProductID");
                double unitPrice = reader.GetDouble("UnitPrice");
                short quantity = reader.GetInt16("Quantity");
                double discount = reader.GetDouble("Discount");
                double calculatedAmount = reader.GetDouble("CalculatedAmount");

                Console.WriteLine("Document " + count);
                Console.WriteLine("CustomerID: " + customerId);
                Console.WriteLine("ProductID: " + productId);
                Console.WriteLine("UnitPrice: " + unitPrice);
                Console.WriteLine("Quantity: " + quantity);
                Console.WriteLine("Discount: " + discount);
                Console.WriteLine("CalculatedAmount: " + calculatedAmount);
                Console.WriteLine("");
                count++;
            }
            reader.Dispose();

            Console.WriteLine("'" + count + "' documents were retrieved");
            Console.WriteLine();
        }

        /// <summary>
        /// This method queries data from the collection. The query uses the clr function
        /// </summary>
        private void QueryWithClrFunctionProvider()
        {
            const string selectQuery = "SELECT EmployeeID, GetFullName(FirstName, LastName) as FullName, GetFullAddress(Address, City, Region, Country) as FullAddress FROM Employees WHERE EmployeeID = 2";

            //Executing Select Query
            IDBCollectionReader reader = _database.ExecuteReader(selectQuery);
            int count = 0;
            while (reader.ReadNext())
            {
                //Get String Attribute from Reader
                int employeeId = reader.GetInt32("EmployeeID");
                string fullName = reader.GetString("FullName");
                string fullAddress = reader.GetString("FullAddress");

                Console.WriteLine("Document " + count);
                Console.WriteLine("EmployeeID: " + employeeId);
                Console.WriteLine("EmployeeName: " + fullName);
                Console.WriteLine("EmployeeAddress: " + fullAddress);
                Console.WriteLine("");
                count++;
            }
            reader.Dispose();

            Console.WriteLine("'" + count + "' documents were retrieved");
        }
    }
}
