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
    /// A sample program that demonstrates the usage of CLR Triggers. The sample CLR Trigger used is defined in 
    /// ProductValidationTrigger class in CLRTriggers project. 
    /// 
    /// Requirements:
    ///     1. A NosDB database and collection
    ///     2. This CLRTriggers project to be built and deployed on NosDB server
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                NosDB.Samples.ClrTriggersUsage clrTriggersUsage = new ClrTriggersUsage();
                clrTriggersUsage.Run();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                Environment.Exit(0);
            }
        }
    }
}
