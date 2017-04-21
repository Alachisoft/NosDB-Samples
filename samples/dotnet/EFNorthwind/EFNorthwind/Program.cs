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
    /// A sample application for using entityframework with northwind database.
    /// 
    /// Requirements:
    ///     1. Northwind database
    ///     2. Nuget package Alachisoft.EntityFrameworkCore.NosDB.Design version 1.0.0.0
    ///     3. A connection string in app.config
    ///     4. Set the xmlPath in dbFirstSection under databaseFirstApproachGroup tag in app.config file of sample to "..\..\NosDBNorthwindSchemaConf.xml"
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                NosDB.Samples.EfNorthwind efNorthwind = new EfNorthwind();
                efNorthwind.Run();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Environment.Exit(0);
        }
    }
}
