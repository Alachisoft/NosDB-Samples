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
    /// A sample program that demonstrates JOINs and Nested Queries in NosDB. 
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
                NosDB.Samples.Joins joins = new Joins();
                joins.Run();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Environment.Exit(0);
        }
    }
}
