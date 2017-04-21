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
    /// A sample application that demonstrates the usage of REST api capabilities of NosDB and performs
    /// CRUD operations through it. 
    /// 
    /// Requirements:
    ///     1. A NosDB database and collection
    ///     2. The REST package has been deployed (see admin guide)
    ///     3. A RequestUri to the existing database in app.config
    /// 
    /// </summary>
    class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                NosDB.Samples.RestDotNet restDotNet = new RestDotNet();
                restDotNet.Run();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            Console.ReadLine();
        }
    }
}
