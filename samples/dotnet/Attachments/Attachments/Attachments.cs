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
using System.IO;
using NosDB.Client;
using NosDB.Client.Attachment;
using NosDB.Common;
using NosDB.Common.Server.Engine;
using NosDB.Samples.EntityObjects;
using System.Collections.Generic;

namespace NosDB.Samples
{
    /// <summary>
    /// Class that provides the functionality of the sample
    /// </summary>
    public class Attachments
    {
        private Database _database;
        private DBCollection<Employee> _employeeCollection;
        private AttachmentStore _attachmentStore;
        private const string BASE_PATH = @"..\..\Pictures\";

        /// <summary>
        /// Executing this method will perform all the operations of the sample
        /// </summary>
        public void Run()
        {
            try
            {
                // Generate new employee
                Employee newEmployee = GenerateEmployee(754640567);

                // Initialize database, collection and attachment store
                InitializeDatabaseAndCollections();

                // Insert employee in employee collection and employee picture in attachment store
                InsertAttachment(newEmployee);

                // Fetch employee picture from attachment collection
                GetAttachment(newEmployee);

                // Update (replace) employee picture in attachment store with a new one
                UpdateAttachment(newEmployee);

                // Query attachment metadata to get employee picture
                QueryAttachments();

                // Delete employee picture from attachment store
                DeleteAttachment(newEmployee);

                // Releasing all the resources
                _database.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        /// <summary>
        /// Initalizes the database using the connection string provided in app.config, gets
        /// employee collection and attachment store
        /// </summary>
        private void InitializeDatabaseAndCollections()
        {
            string conectionString = ConfigurationManager.AppSettings["ConnectionString"];
			string collection = ConfigurationManager.AppSettings["CollectionName"];
			
            _database = Client.NosDBClient.GetDatabase(conectionString);

            // Get instance of collection from the database
            _employeeCollection = _database.GetDBCollection<Employee>(collection);

            // Get an instance of the Attachment store from the database that has enabled attachments:
            _attachmentStore = _database.GetAttachmentStore();
        }

        /// <summary>
        /// This method inserts an employee in the employee collection and then inserts a picture of 
        /// the employee in the attachment store
        /// </summary>
        /// <param name="employee"> Employee that needs to be inserted in the collection </param>
        public void InsertAttachment(Employee employee)
        {
           // Insert employee into the database
            _employeeCollection.InsertDocument(employee, WriteConcern.InMemory);

            // Create a metadata document. This is an optional document which can contain any user specified
            // information about the attachment. Consequently, an null can also be passed in its place.
            // This user specified metadata can be used in queries to fetch attachments
            var metadata = new JSONDocument { { "fileType", "BMP" }, { "info", "Employee picture" } };

            // AttachmentId to be inserted. This acts as a unique identifier for the attachment. 
            string attachmentId = GenerateAttachmentId(employee.EmployeeID);

            // Insert a picture of an employee in database.
            using (var fileStream = new FileStream(BASE_PATH + "2.BMP", FileMode.Open, FileAccess.Read))
            {
                _attachmentStore.InsertAttachment(attachmentId, fileStream, metadata);
            }

            Console.WriteLine("Attachment inserted into the database.");
        }

        /// <summary>
        /// This method fetches the employee from the employee collection and then gets their picture
        /// from the attachment store
        /// </summary>
        /// <param name="employee"> Employee whos employeeId will be used to fetch attachment from attachment
        /// store </param>
        public void GetAttachment(Employee employee)
        {
            // Fetch the inserted Employee
            Employee fetchedEmployee = _employeeCollection.GetDocument(employee.EmployeeID);

            // Get attachment from database against the employee fetched...
            // It will return null if attachment does not exist in the database.
            Attachment attachment = _attachmentStore.GetAttachment(GenerateAttachmentId(fetchedEmployee.EmployeeID));
            if (attachment != null)
            {
                // Get Metadata stored with the attachment.
                Console.WriteLine("Metadata : " + attachment.Metadata);

                // Get stream from attachment
                using (var stream = attachment.GetStream())
                {
                    // Read data from the stream.
                    var dataBytes = new byte[stream.Length];
                    stream.Read(dataBytes, 0, (int)stream.Length);

                    // Add photo to employee object
                    fetchedEmployee.Photo = dataBytes;
                    using (var fileStream = new FileStream("RetrievedImage.BMP", FileMode.Create))
                    {
                        fileStream.Write(dataBytes, 0, dataBytes.Length);
                    }
                }

                Console.WriteLine("Attachment retrieved from database.");
            }
            else
            {
                Console.WriteLine("Attachment does not exist in the database.");
            }
        }

        /// <summary>
        /// This method updates (replaces) the attachment in the attachment store of the database for the 
        /// given employee
        /// </summary>
        /// <param name="employee"> Employee whos employeeId will be used to update (replace) attachment in the
        /// attachment store </param>
        public void UpdateAttachment(Employee employee)
        {
            // Get the attachment id to replace the attachment with some other file.
            string attachmentIdToBeReplaced = GenerateAttachmentId(employee.EmployeeID);

            // Create new metadata for replacment. Not doing so will result in the existing metadata to be replaced with null. 
            var newMetadata = new JSONDocument { { "fileType", "BMP" }, { "info", "New Employee picture" } };

            // Replace attachment in the database
            using (var fileStream = new FileStream(BASE_PATH + "1.BMP", FileMode.Open, FileAccess.Read))
            {
                _attachmentStore.UpdateAttachment(attachmentIdToBeReplaced, fileStream, newMetadata);
            }

            Console.WriteLine("Attachment replaced into the database.");
        }

        /// <summary>
        /// This method fetches the attachments from the attachment store using query on the metadata
        /// </summary>
        public void QueryAttachments()
        {
            // It will return null if attachment does not exist in the database.
            List<Attachment> attachments = _attachmentStore.GetAttachments("SELECT * FROM attachments WHERE UserMetadata.info = 'New Employee picture'");

            if (attachments != null && attachments.Count > 0)
            {
                Console.WriteLine("Attachments found = " + attachments.Count);

                foreach (Attachment attachment in attachments)
                {
                    // Get Metadata stored with the attachment.
                    Console.WriteLine("Metadata : " + attachment.Metadata);

                    // Get stream from attachment
                    using (var stream = attachment.GetStream())
                    {
                        // Read data from the stream.
                        var dataBytes = new byte[stream.Length];
                        stream.Read(dataBytes, 0, (int)stream.Length);

                        using (var fileStream = new FileStream("RetrievedImage.BMP", FileMode.Create))
                        {
                            fileStream.Write(dataBytes, 0, dataBytes.Length);
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Attachments does not exist in the database.");
            }
        }

        /// <summary>
        /// This method deletes the attachment from the attachment store for the given employee
        /// </summary>
        /// <param name="employee"> Employee whos employeeId will be used to remove attachment from the 
        /// attachment store</param>
        public void DeleteAttachment(Employee employee)
        {
            // Delete the employee added
            _employeeCollection.DeleteDocument(employee.EmployeeID, WriteConcern.InMemory);

            // Generate attachmentId to be deleted
            string attachmentId = GenerateAttachmentId(employee.EmployeeID);

            // Delete attachment from the database
            _attachmentStore.DeleteAttachment(attachmentId);

            Console.WriteLine("Attachment deleted from the database.");
        }

        /// <summary>
        /// Generates new instance of <see cref="Employee"/> with the given emplyeeId
        /// </summary>
        /// <param name="employeeId"> employeeId that will be used to generate emplyee </param>
        /// <returns> returns new instance of <see cref="Employee"/></returns>
        private Employee GenerateEmployee(long employeeId)
        {
            // Create an employee
            var newEmployee = new Employee
            {
                EmployeeID = employeeId,
                FirstName = "John",
                LastName = "Doe",
                Title = "Sales Representative",
                TitleOfCourtesy = "Mr."
            };
            return newEmployee;
        }

        /// <summary>
        /// Generate attachmentId the given employeeId
        /// </summary>
        /// <param name="employeeId"> employeeId for which attachmentId will be generated </param>
        /// <returns> returns attachmentId for the given employeeId </returns>
        private string GenerateAttachmentId(long employeeId)
        {
            return "Employee:" + employeeId;
        }
    }
}
