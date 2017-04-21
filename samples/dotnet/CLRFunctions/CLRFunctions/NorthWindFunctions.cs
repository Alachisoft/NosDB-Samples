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
using NosDB.Common.Queries.UserDefinedFunction;

namespace NosDB.Samples
{
    public class CalculateAmountUdf
    {
	    /// ******************************************************************************
        /// <summary>
        /// A sample UDF definition. A User Defined Function in NosDB can be defined through the UserDefinedFunction()
        /// attribute on a function. The argument of the attribute is used to specify the identifier for that function
        /// which is to be expected in the query. The user must ensure the correct number of arguments when querying.
        /// 
        /// Requirements:
        ///     1. A NosDB database and collection
        ///     2. This assembly to be built and deployed on NosDB server
        /// </summary>
        /// 
        /// <returns></returns>
        [UserDefinedFunction("CalculateAmount")]
        public static double CalculateAmount(double unitPrice, long quantity, double discount)
        {
            if (discount < 0 || discount > 1)
            {
                throw new ArgumentException("Invalid parameter value provided, parameter \"discount\".");
            }

            if (discount.CompareTo(0) == 0)
            {
                return unitPrice * quantity;
            }

            return (unitPrice - (unitPrice / 100) * discount) * quantity;
        }
    }
}
