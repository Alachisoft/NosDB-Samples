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
using System.Xml.Serialization;
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
    public class BasicSql
    {
        private Database _database;

        /// <summary>
        /// Executing this method will perform all the operations of the sample
        /// </summary>
        public void Run()
        {
            // Generate a new customer
            Customer customer = GenerateCustomer("Customer1");

            // Initialize database for further operations
            InitializeDatabase();

            // Insert document using query in the collection
            InsertDocumentUsingQuery(customer);

            // Fetch document using query from the collection
            GetDocumentsUsingQuery(customer);

            // Update document using query in the collection
            UpdateDocumentsUsingQuery(customer);

            // Delete document using query from the collection
            DeleteDocumentsUsingQuery(customer);

            // Releasing the resources
            _database.Dispose();
        }

        /// <summary>
        /// This method initalizes the database using the connection string provided in app.config
        /// </summary>
        private void InitializeDatabase()
        {
            string conectionString = ConfigurationManager.AppSettings["ConnectionString"];

            // Initialize an instance of the database to begin performing operations:
            _database = Client.NosDBClient.GetDatabase(conectionString);
        }

        /// <summary>
        /// This method inserts a customer in the collection using query
        /// </summary>
        /// <param name="customer"> Customer that will be inserted </param>
        private void InsertDocumentUsingQuery(Customer customer)
        {
            // Insert Customer 'John Doe' into the 'Customers' Collection using Insert Query.
            const string insertQuery = "INSERT INTO Customers (CustomerID, ContactName, Address, City) VALUES (@customerId, @contactName, @customerAddress, @customerCity)";

            IList<IParameter> insertParameters = new List<IParameter>();
            insertParameters.Add(new Parameter("customerId", customer.ID));
            insertParameters.Add(new Parameter("contactName", customer.ContactName));
            insertParameters.Add(new Parameter("customerAddress", customer.Address));
            insertParameters.Add(new Parameter("customerCity", customer.City));

            // Executing Insert Query
            _database.ExecuteNonQuery(insertQuery, insertParameters);

            Console.WriteLine("Inserted a customer in the database");
        }

        /// <summary>
        /// This method fetches data from the collection
        /// </summary>
        /// <param name="customer"> Customer whos ID will be used to fetch data </param>
        private void GetDocumentsUsingQuery(Customer customer)
        {
            // Get the Customer 'John Doe' from the 'Customers' Collection using the Select Query
            Console.WriteLine("Getting ContactName and Address of customer 'John Doe' using SELECT query");
            const string selectQuery = "SELECT ContactName, Address FROM Customers WHERE ContactName = @contactName";

            IList<IParameter> selectParameters = new List<IParameter>();
            selectParameters.Add(new Parameter("contactName", customer.ContactName));

            // Executing Select Query
            IDBCollectionReader reader = _database.ExecuteReader(selectQuery, selectParameters);
            // Iterating over the reader
            int totalRecords = 0;
            while (reader.ReadNext())
            {
                // Get String Attribute from Reader
                string customerName = reader.GetString("ContactName");

                // Get Complete Customer form Reader
                Customer fetchedCustomer = reader.GetObject<Customer>();
                totalRecords++;
            }
            reader.Dispose();

            Console.WriteLine("'" + totalRecords + "' records are present in the collection");
        }

        /// <summary>
        /// This method updates customer in the collection
        /// </summary>
        /// <param name="customer"> Customer whos contact name will be used to update data in the collection </param>
        private void UpdateDocumentsUsingQuery(Customer customer)
        {
            // Update 'John Doe' in the 'Customers' Collection using Update Query.
            Console.WriteLine("Updating Address and City of customer 'John Doe' using update query");
            const string updateQuery = "UPDATE Customers SET (Address = @address, City = @city) WHERE ContactName = @contactName";
            IList<IParameter> updateParametes = new List<IParameter>();

            updateParametes.Add(new Parameter("address", "120 Hanover Sq."));
            updateParametes.Add(new Parameter("city", "London"));
            updateParametes.Add(new Parameter("contactName", customer.ContactName));

            // Executing Update Query
            long rowsAffected = _database.ExecuteNonQuery(updateQuery, updateParametes);

            Console.WriteLine("'" + rowsAffected + "' records were updated!");
        }

        /// <summary>
        /// This method deletes the customer that was previously inserted
        /// </summary>
        /// <param name="customer"> Customer whos contact name will used to delte data from collection </param>
        private void DeleteDocumentsUsingQuery(Customer customer)
        {
            // Delete 'John Doe' from the Customers Collection using Delete Query.
            Console.WriteLine("Deleting customer 'John Doe' using delete query");

            const string deleteQuery = "DELETE FROM Customers WHERE ContactName = @contactName";
            IList<IParameter> deleteParametes = new List<IParameter>();
            deleteParametes.Add(new Parameter("contactName", customer.ContactName));

            // Executing Delete Query
            long rowsAffected = _database.ExecuteNonQuery(deleteQuery, deleteParametes);

            Console.WriteLine("'" + rowsAffected + "' records were deleted!");
        }

        /// <summary>
        /// This method creates a new instance of <see cref="Customer"/>
        /// </summary>
        /// <param name="customerId"> The instance generated will have this ID </param>
        /// <returns> Returns a new instance of <see cref="Customer"/></returns>
        private Customer GenerateCustomer(string customerId)
        {
            var customer = new Customer();
            customer.ID = customerId;
            customer.ContactName = "John Doe";
            customer.Address = "Obere Street 57";
            customer.City = "Berlin";
            return customer;
        }
    }
}
