// ===============================================================================
// Alachisoft (R) NosDB Sample Code.
// ===============================================================================
// Copyright © Alachisoft.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================

using NosDB.Client;
using NosDB.Common;
using NosDB.Common.MapReduce;
using NosDB.Common.Server.Engine;
using NosDB.Samples.EntityObjects;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace NosDB.Samples
{
    /// <summary>
    /// Class that provides the functionality of the sample
    /// </summary>
    public class MapReduce
    {
        private Database _database;
        private DBCollection<Order> _ordersCollection;
        private const string OUTPUT_COLLECTION_NAME = "mr_output_collection";

        /// <summary>
        /// Executing this method will perform all the operations of the sample
        /// </summary>
        public void Run()
        {
            // Generate sample orders data to be used in this sample
            ICollection<Order> orders = GenerateOrders();

            // Initialize database and collection
            InitializeDatabaseAndCollection();

            // Insert some documents in the collection so that map reduce can run on it
            InsertDocuments(orders);

            // Execute map reduce task and get tracker
            ITrackableTask trackableTask = ExecuteMapReduceTask();

            // Fetch result and iterate the result
            FetchAndReadTaskResult(trackableTask);

            // Releasing the resources
            _database.Dispose();
        }

        /// <summary>
        /// This method initalizes the database and the collection using the connection string specified in app.config
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
            //Initialize an instance of the database to begin performing operations:
            _database = Client.NosDBClient.GetDatabase(connectionString);
             
            //Get an instance of the 'Orders' collection from the database:
            _ordersCollection = _database.GetDBCollection<Order>(collection);
        }

        /// <summary>
        /// This method inserts some data in the collection
        /// </summary>
        /// <param name="orders"> Orders that will be inserted during the operation </param>
        private void InsertDocuments(ICollection<Order> orders)
        {
            //Add some sample data to run the task on
            List<FailedDocument> failedDocsOnInsert = _ordersCollection.InsertDocuments(orders, WriteConcern.InMemory);

            if (failedDocsOnInsert == null || failedDocsOnInsert.Count == 0)
                Console.WriteLine("All items inserted into the '{0}' collection successfully. \n", _ordersCollection.Name);
            else
                Console.WriteLine("One or more items could not be inserted to the '{0}' collection.\n", _ordersCollection.Name);
        }

        /// <summary>
        /// This method executes the map reduce task
        /// </summary>
        /// <returns> Returns the tracker through which task can be monitored and result can be fetched </returns>
        private ITrackableTask ExecuteMapReduceTask()
        {
            // Create InitParams for the MapReduce task, i.e., set mapper, combiner and reducer
            // Mapper is a must, other two are optional.
            MapReduceInitParams initParams = new MapReduceInitParams("NosDB.Samples.MapReduceImplementation.Mapper, NosDB.Samples.MapReduceImplementation, Version=1.0.0.0, Culture=neutral", "mapreduceimpl");
            initParams.Combiner = "NosDB.Samples.MapReduceImplementation.CombinerFactory, NosDB.Samples.MapReduceImplementation, Version=1.0.0.0, Culture=neutral";
            initParams.Reducer = "NosDB.Samples.MapReduceImplementation.ReducerFactory, NosDB.Samples.MapReduceImplementation, Version=1.0.0.0, Culture=neutral";

            // Specify the output option, It will create the collection if it does not exist.
            initParams.OutputOption = NosDB.Common.MapReduce.MROutputOption.CUSTOM_COLLECTION;
            // You have to specify the custom collection name if the MROutputOption is set to CUSTOM_COLLECTION,
            // For DEFAULT_COLLECTION, collection will be created as Collection_<Some_GUID>
            initParams.CustomCollectionName = OUTPUT_COLLECTION_NAME;

            // Specify the collections, on which you want to execute the map reduce task.
            List<string> collections = new List<string>();
            collections.Add("orders");

            // The call which sends the task to the NosDB Server, and returns a trackable object.
            // BEFORE EXECUTION:
            // Make sure that you have deployed the implementation of mapreduce task (Mapper, Combiner and Reducer).
            ITrackableTask trackableTask = _database.ExecuteTask(initParams, collections);

            return trackableTask;
        }

        /// <summary>
        /// This method fetches the map reduce task result from the database and iterates the result
        /// </summary>
        /// <param name="trackableTask"> TrackableTask whos result will be fetched and iterated </param>
        private void FetchAndReadTaskResult(ITrackableTask trackableTask)
        {
            // Call to get the result, this is a BLOCKING call... (it waits until task completes and returns the result...)
            ITaskResult taskResult = trackableTask.GetResult();

            //NOTE: There are other ways you can get the task result, Read more on the documentation...

            IDBCollectionReader resultEnumerator = taskResult.GetEnumerator();
            // Get and enumerate the result.
            while (resultEnumerator.ReadNext())
            {
                //Reading the output
                IJSONDocument document = resultEnumerator.GetDocument();
                Console.WriteLine("Name: " + document["Key"] + " and Count available: " + document["Value"]);
            }


            // Now that we have gathered the data
            // we can query the top results.

            IDBCollectionReader rdr = _database.ExecuteReader("SELECT TOP 5 * FROM \"" + OUTPUT_COLLECTION_NAME + "\" ORDER BY Value DESC");
            while (rdr.ReadNext())
            {
                //Reading the output
                IJSONDocument document = rdr.GetDocument();
                Console.WriteLine("Name: " + document["Key"] + " and Count available: " + document["Value"]);
            }
        }

        /// <summary>
        /// This method generates a list of <see cref="Order"/> class
        /// </summary>
        /// <returns> Returns a list of <see cref="Order"/></returns>
        private ICollection<Order> GenerateOrders()
        {
            ICollection<Order> orders = new List<Order>();
            orders.Add(new Order() { ID = 1, OrderDetails = new List<OrderDetail>(), ShipCity = "London", });
            orders.Add(new Order() { ID = 2, OrderDetails = new List<OrderDetail>(), ShipCity = "New York", });
            orders.Add(new Order() { ID = 3, OrderDetails = new List<OrderDetail>(), ShipCity = "Sydney", });
            orders.Add(new Order() { ID = 4, OrderDetails = new List<OrderDetail>(), ShipCity = "London", });
            orders.Add(new Order() { ID = 5, OrderDetails = new List<OrderDetail>(), ShipCity = "New York", });
            return orders;
        }
    }
}
