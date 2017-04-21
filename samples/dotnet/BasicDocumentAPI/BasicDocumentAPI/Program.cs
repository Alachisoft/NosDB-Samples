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

namespace NosDB.Samples
{
	/// ******************************************************************************
    /// <summary>
    /// A sample program that demonstrates how to use the NosDB CRUD api. 
    /// NosDB also has an api to interact with the data apart from query.
    /// In this sample some of the operations that can be performed using this api is demonstrated.
    /// 
    /// Requirements:
    ///     1. A NosDB database and collection
    ///     2. A connection string in app.config
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                NosDB.Samples.BasicDocumentApi basicDocumentApi = new BasicDocumentApi();
                basicDocumentApi.Run();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
