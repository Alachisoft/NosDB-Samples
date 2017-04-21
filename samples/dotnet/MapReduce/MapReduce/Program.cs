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
    /// A sample program that demonstrates the use of MapReduce in NosDB. 
    /// 
    /// Requirements
    ///     1. A NosDB database and collection
    ///     2. A MapReduce task implementation to be deployed on server side
    ///     3. A connection string in app.config
    /// 
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                NosDB.Samples.MapReduce mapReduce = new MapReduce();
                mapReduce.Run();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            Environment.Exit(0);
        }

    }
}
