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
using NosDB.Common.Server.Engine;
using NosDB.Samples.EntityObjects;


namespace NosDB.Samples
{
    /// <summary>
    /// Class that provides the functionality of the sample
    /// </summary>
    public class RestDotNet
    {
        private RestApi _restApi;
        private string _requestUriString;

        /// <summary>
        /// Executing this method will perform all the operations of the sample
        /// </summary>
        public void Run()
        {
            // Generate customer to be used during the sample
            Customer customer1 = GenerateCustomer();

            // Initialize rest api
            InitializeRestApi();

            // Insert Customer 'John Doe' into the 'Customers' Collection using HTTP POST Request.
            Console.WriteLine("Inserting Customer..");
            _restApi.Post(_requestUriString, customer1);
            Console.WriteLine();

            // Get 'John Doe' from the Customers Collection using HTTP GET Request.
            Console.WriteLine("Getting Customer..");
            Customer customer = _restApi.Get(_requestUriString, "Customer1");
            PrintCustomerDetails(customer);
            Console.WriteLine();

            // Update 'John Doe' into Customers Collection using HTTP PUT Request.
            Console.WriteLine("Updating Customer..");
            var updatedCustomer = new Customer();
            updatedCustomer.ID = "Customer1";
            updatedCustomer.ContactName = "John Doe";
            // Update Address to Keskuskatu 45
            updatedCustomer.Address = "Keskuskatu 45";
            // Update City from Berlin to Helsinki
            updatedCustomer.City = "Helsinki";
            _restApi.Put(_requestUriString, "Customer1", updatedCustomer);
            Console.WriteLine();

            // Executing Query on Customers Collection using HTTP POST Request.
            Console.WriteLine("Executing Query..");
            int numberOfRecords = _restApi.ExecuteQuery(_requestUriString, "Select * FROM Customers",
                new List<IParameter>());
            Console.WriteLine("Number of Documents Retrieved:" + numberOfRecords);
            Console.WriteLine();

            // Delete 'John Doe' from the Customers Collection using HTTP DELETE Request.
            Console.WriteLine("Deleting Customer..");
            _restApi.Delete(_requestUriString, "Customer1");
        }

        private void InitializeRestApi()
        {
            // We packaged all the generic REST methods in this class.
            _restApi = new RestApi();

            _requestUriString = ConfigurationManager.AppSettings["RequestUri"];

            if (String.IsNullOrEmpty(_requestUriString))
            {
                throw new Exception("The Request URI string cannot be null or empty.");
            }
        }

        private Customer GenerateCustomer()
        {
            var customer = new Customer
            {
                ID = "Customer1",
                ContactName = "John Doe",
                Address = "Obere Street 57",
                City = "Berlin"
            };
            return customer;
        }

        private void PrintCustomerDetails(Customer customer)
        {
            if (customer == null)
                return;
            Console.WriteLine("CustomerID : " + customer.ID);
            Console.WriteLine("ContactName : " + customer.ContactName);
            Console.WriteLine("CustomerAddress : " + customer.Address);
            Console.WriteLine("CustomerCity : " + customer.City);
        }
    }
}
