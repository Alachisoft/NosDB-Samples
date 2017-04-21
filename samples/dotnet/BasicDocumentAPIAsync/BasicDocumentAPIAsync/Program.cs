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
    /// A sample program that demonstrates how to utilize the async CRUD api in NosDB. The api requires a callback method
    /// which is invoked once the async operation is performed. 
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
                NosDB.Samples.BasicDocumentApiAsync basicDocumentApiAsync = new BasicDocumentApiAsync();
                basicDocumentApiAsync.Run();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
