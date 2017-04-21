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
    /// A sample program that demonstrates the usage of LINQ with NosDB. 
    /// 
    /// Requirements:
    ///     1. A NosDB database and collection
    ///     2. A connection string in app.config
    /// 
    /// </summary>
    class Program
    {
	    private static void Main(string[] args)
	    {
	        try
	        {
                NosDB.Samples.BasicLINQ basicLinq = new BasicLINQ();
                basicLinq.Run();
	        }
	        catch (Exception e)
	        {
	            Console.WriteLine(e.Message);
	        }
	    }
    }
}
