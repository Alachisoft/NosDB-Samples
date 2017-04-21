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
using System.IO;
using System.Net;
using System.Text;
using NosDB.Common;
using NosDB.Common.Server.Engine;
using NosDB.Common.Server.Engine.Impl;
using NosDB.Samples.EntityObjects;
using Newtonsoft.Json;

namespace NosDB.Samples
{
    /// <summary>
    /// A sample class that provides the REST methods. 
    /// </summary>
    public class RestApi
    {
        private int _numberOfRecords;

        public Customer Get(string requestUri, string documentKey)
        {
            //Sample HTTP Request URI To Delete a Document
            //http://localhost/nosdbdata/customers('1') get document from customers collection having document key '1'.
            requestUri = requestUri + "('" + documentKey + "')";

            //Create Web Request
            WebRequest request = WebRequest.Create(requestUri);

            //Set Request Method to GET
            request.Method = "GET";

            WebResponse response;
            try
            {
                //Send request and get HTTP response.
                response = request.GetResponse();
            }
            catch (WebException exception)
            {
                PrintErrorResponse(exception);
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

            //Get HTTP Response Status
            var webResponse = (HttpWebResponse)response;
            switch (webResponse.StatusCode)
            {
                case HttpStatusCode.OK:
                    string responseBody = GetResponseBody(webResponse);
                    return JsonConvert.DeserializeObject<Customer>(responseBody);
            }
            //close the response.
            response.Close();

            return null;
        }

        public void Post(string requestUri, Customer customer)
        {
            //Create Web Request
            WebRequest request = WebRequest.Create(requestUri);
            //Set Request Method to POST
            request.Method = "POST";

            //JSON Serialize Customer
            string serialziedCustomer = JsonConvert.SerializeObject(customer);

            //Customer Data which will be inserted in 'Customers' collection
            string postData = serialziedCustomer;
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            //Set Content-Type to "application/json"
            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;

            //Get Request stream and write data..
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            WebResponse response;

            try
            {
                //Send request and get HTTP response.
                response = request.GetResponse();
            }
            catch (WebException exception)
            {
                PrintErrorResponse(exception);
                //Console.WriteLine(exception.Message);
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            //Get HTTP Response Status
            var webResponse = (HttpWebResponse)response;
            switch (webResponse.StatusCode)
            {
                case HttpStatusCode.OK:
                    Console.WriteLine("Insert Successfully");
                    break;
            }
            //close the response.
            response.Close();
        }

        public void Delete(string requestUri, string documentKey)
        {
            //Sample HTTP Request URI To Delete a Document
            //http://localhost/nosdbdata/customers('1') document with document key '1' will be deleted from customers collection.
            requestUri = requestUri + "('" + documentKey + "')";

            //Create Web Request
            WebRequest request = WebRequest.Create(requestUri);

            //Set Request Method to DELETE
            request.Method = "DELETE";

            WebResponse response;
            try
            {
                //Send request and get HTTP response.
                response = request.GetResponse();
            }
            catch (WebException exception)
            {
                PrintErrorResponse(exception);
                //Console.WriteLine(exception.Message);
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            //Get HTTP Response Status
            var webResponse = (HttpWebResponse)response;
            switch (webResponse.StatusCode)
            {
                case HttpStatusCode.OK:
                    Console.WriteLine("Deleted Successfully");
                    break;
            }
            //close the response.
            response.Close();
        }

        public void Put(string requestUri, string documentKey, Customer updatedCustomer)
        {
            //Sample HTTP Request URI To Delete a Document
            //http://localhost/nosdbdata/customers('1') update document in customers collection having document key '1'.
            requestUri = requestUri + "('" + documentKey + "')";

            //Create Web Request
            WebRequest request = WebRequest.Create(requestUri);

            //Set Request Method to PUT
            request.Method = "PUT";

            //JSON Serialize Customer
            string serialziedCustomer = JsonConvert.SerializeObject(updatedCustomer);

            //Customer Data which will be updated in 'Customers' collection
            string putData = serialziedCustomer;
            byte[] byteArray = Encoding.UTF8.GetBytes(putData);

            //Set Content-Type to "application/json"
            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;

            //Get Request stream and write data..
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            WebResponse response;

            try
            {
                //Send request and get HTTP response.
                response = request.GetResponse();
            }
            catch (WebException exception)
            {
                PrintErrorResponse(exception);
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            //Get HTTP Response Status
            var webResponse = (HttpWebResponse)response;
            switch (webResponse.StatusCode)
            {
                case HttpStatusCode.OK:
                    Console.WriteLine("Updated Successfully");
                    break;
            }
            //close the response.
            response.Close();
        }


        public int ExecuteQuery(string requestUri, string queryText, List<IParameter> parameter)
        {
            _numberOfRecords = 0;
            //Create Web Request
            WebRequest request = WebRequest.Create(requestUri);

            //Set Request Method to POST. POST is used to Execute Query also.
            request.Method = "POST";

            var query = new Query { QueryText = queryText, Parameters = parameter };

            //Json Serialize Query
            string serializedQuery = JsonConvert.SerializeObject(query);

            string postData = serializedQuery;
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            //Content-Type must be set to application/nosquery.
            request.ContentType = "application/nosquery";
            request.ContentLength = byteArray.Length;

            //Get Request stream and write data..
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            WebResponse response;

            try
            {
                //Send request and get HTTP response.
                response = request.GetResponse();
            }
            catch (WebException exception)
            {
                PrintErrorResponse(exception);
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }

            //Get Response Stream to Read Data from Response Body.
            dataStream = response.GetResponseStream();

            if (dataStream != null)
            {
                var reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                var responseDocument = JSONDocument.Parse(responseFromServer);

                if (responseDocument != null)
                {
                    string serializedData = responseDocument.GetString("value");  //Query Result Data in JSON Array
                    var customers = JsonConvert.DeserializeObject<Customer[]>(serializedData);  //Deserialize JSON Array to Customer Array.
                    _numberOfRecords += customers.Length;
                    //Perform Operations
                }
                reader.Close();
            }
            if (dataStream != null) dataStream.Close();

            //Retrieve Query Result Set Information From Response Header.
            //This information is required to GetNextChunk.
            string readerId = response.Headers.Get("ReaderId");
            string isLastChunk = response.Headers.Get("IsLastChunk");
            string chunkId = response.Headers.Get("ChunkId");
            response.Close();

            if (isLastChunk.Equals("False", StringComparison.OrdinalIgnoreCase)) //Check if this is a last chunk of data.
            {
                //If IsLastChunk is false then GetNextChunk of Data.
                //Else Complete Data is retrieved.
                GetNextChunk(requestUri, readerId, Convert.ToInt32(chunkId));
            }
            return _numberOfRecords;
        }

        public void GetNextChunk(string requestUri, string readerId, int lastChunkId)
        {
            //Create Web Request
            WebRequest request = WebRequest.Create(requestUri);
            //Set Request Method to POST. POST is used for GetChunk also.
            request.Method = "POST";

            //Information Required to GetNextChunk. 
            var jsonDocument = new JSONDocument { { "ReaderId", readerId }, { "LastChunkId", lastChunkId } };
            string postData = jsonDocument.ToString();
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            //Content-Type must be set to application/getchunk.
            request.ContentType = "application/getchunk";
            request.ContentLength = byteArray.Length;

            //Get Request stream and write data.. Data written in the stream is a body of Request.
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            WebResponse response;
            try
            {
                //Send request and get HTTP response.
                response = request.GetResponse();
            }
            catch (WebException exception)
            {
                PrintErrorResponse(exception);
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            dataStream = response.GetResponseStream();
            if (dataStream != null)
            {
                var reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                var responseDocument = JSONDocument.Parse(responseFromServer);

                if (responseDocument != null)
                {
                    string temp = responseDocument.GetString("value");  //Query Result Data in JSON Array
                    var customers = JsonConvert.DeserializeObject<Customer[]>(temp);  //Deserialize JSON Array to Customer Array.
                    _numberOfRecords += customers.Length;
                    //Perform Operations
                }

                reader.Close();
            }
            if (dataStream != null) dataStream.Close();

            //Retrieve Query Result Set Information From Response Header.
            //This information is required to GetNextChunk.
            readerId = response.Headers.Get("ReaderId");
            string isLastChunk = response.Headers.Get("IsLastChunk");
            string chunkId = response.Headers.Get("ChunkId");
            response.Close();

            if (isLastChunk.Equals("False", StringComparison.OrdinalIgnoreCase)) //Check if this is a last chunk of data.
            {
                //If IsLastChunk is false then GetNextChunk of Data.
                //Else Complete Data is retrieved.
                GetNextChunk(requestUri, readerId, Convert.ToInt32(chunkId));
            }
        }


        private string GetResponseBody(HttpWebResponse response)
        {
            //Get Response Body.
            string responseJson = string.Empty;
            Stream dataStream = response.GetResponseStream();

            if (dataStream != null)
            {
                var reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();

                //Response Body will be in JSON Form.
                IJSONDocument responseDocument = JSONDocument.Parse(responseFromServer);
                if (responseDocument != null && responseDocument.Contains("value"))
                {
                    responseJson = responseDocument.GetString("value");  //Data will be in value
                }
                reader.Close();
            }
            if (dataStream != null) dataStream.Close();
            return responseJson;
        }

        private string GetErrorResponseBody(HttpWebResponse response)
        {
            //Get Error Response Body.
            string responseJson = string.Empty;
            Stream dataStream = response.GetResponseStream();

            if (dataStream != null)
            {
                var reader = new StreamReader(dataStream);
                responseJson = reader.ReadToEnd();
                reader.Close();
            }
            if (dataStream != null) dataStream.Close();
            return responseJson;
        }

        private void PrintErrorResponse(WebException exception)
        {
            var response = (HttpWebResponse)exception.Response;
            switch (response.StatusCode)
            {
                case HttpStatusCode.InternalServerError:  //As 500 status code is sent with Error Message in Body from NosDB REST Server in case of Exception.
                    string responseBody = GetErrorResponseBody(response);
                    Console.WriteLine(responseBody);
                    break;
                default:
                    Console.WriteLine(exception.Message);
                    break;
            }
        }
    }
}
