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
    /// Alachisoft Sample ADO.NET Program for NosDB
    /// 
    /// A sample program that demonstrates how to access NosDB through ADO.NET Provider. This
    /// program shows how to use SELECT statement to fetch a "reader" and iterate over it. It 
    /// also shows how to use INSERT, UPDATE, and DELETE statements in ADO.NET.
    /// 
    /// Requirements:
    ///     1. A Northwind database provided with NosDB
    ///     2. A connection string in App.config
    ///     3. EntityObjects project containing "domain objects" for Northwind
    /// 
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                NosDB.Samples.BasicADONET basicAdonetSample = new BasicADONET();
                basicAdonetSample.Run();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
