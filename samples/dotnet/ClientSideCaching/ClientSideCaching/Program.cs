// ===============================================================================
// Alachisoft (R) NosDB Sample Code.
// ===============================================================================
// Copyright ©  2017 Alachisoft.  All rights reserved.
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
    /// A sample program that demonstrates how to cache the result set of a NosDB query in an external cache through a cache provider. 
    /// The default and recommended cache provider is of NCache. To configure it, see the programmers guide.
    /// 
    /// Requirements:
    ///     1. A NosDB database and collection
    ///     2. An NCache cache
    ///     3. Sample Northwind data to be loaded in the collection
    ///     4. A connection string in app.config
    /// 
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                NosDB.Samples.CachingQueries cachingQueries = new CachingQueries();
                cachingQueries.Run();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                Environment.Exit(0);
            }
        }
    }
}
