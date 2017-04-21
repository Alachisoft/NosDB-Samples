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
    /// A sample program that demonstrates the usage of Attachments in NosDB. 
    /// The Attachments CRUD api is demonstrated. The api uses streams for this purpose. 
    /// 
    /// Attachments are used for storing binary data (BLOBS) in NosDB such as images, audio etc.
    /// 
    /// Requirements:
    ///     1. Attachments is enabled on database.
    ///     2. A valid connection string in app.config
    /// 
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                NosDB.Samples.Attachments attachmentsSample = new Attachments();
                attachmentsSample.Run();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}
