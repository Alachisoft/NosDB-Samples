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
    /// A sample program that demonstrates the usage of SQL dependency in NosDB. The feature allows the user
    /// to be notified if any change occurs in any of the document that falls in the criteria of a specified SQL query.
    /// The features requires a callback that is invoked if a change is detected. 
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
                NosDB.Samples.SqlDependency sqlDependency = new SqlDependency();
                sqlDependency.Run();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\n");
            }
            // You can use SQL Notifications with Single and the Bulk APIs as well.
        }
    }
}
