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
using NosDB.Samples;

namespace NosDB.BasicSQL_Normalized
{
	/// ******************************************************************************
    /// <summary>
    /// A sample program that demonstrates how to perform CRUD operations on a NosDB collection through queries.
    /// This sample also utilizes the NosDB normalized data model.
    /// 
    ///  Requirements:
    ///     1. A NosDB database and collection
    ///     2. A connection string in app.config
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                NosDB.Samples.BasicSql basicqSql = new BasicSql();
                basicqSql.Run();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}
