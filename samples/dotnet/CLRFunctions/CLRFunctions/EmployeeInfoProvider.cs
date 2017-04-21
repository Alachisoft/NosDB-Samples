using NosDB.Common.Queries.UserDefinedFunction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NosDB.Samples
{
    public class EmployeeInfoProvider : IUserDefinedFunctionProvider
    {/// ******************************************************************************
        /// <summary>
        /// A sample UDF definition. This method return the methods implemented in this provider
        /// 
        /// Requirements:
        ///     1. A NosDB database and collection
        ///     2. This assembly to be built and deployed on NosDB server
        /// </summary>
        /// 
        /// <returns></returns>
        public string[] GetUdfInfo()
        {
            return new[] { "getfullname", "getfulladdress" };
        }

        /// ******************************************************************************
        /// <summary>
        /// A sample UDF definition. This method is called by NosDB with function name and its parameters
        /// 
        /// Requirements:
        ///     1. A NosDB database and collection
        ///     2. This assembly to be built and deployed on NosDB server
        /// </summary>
        /// 
        /// <returns></returns>
        public object Execute(string functionName, object[] parameters)
        {
            switch (functionName.ToLower())
            {
                case "getfullname":
                    return GetFullName(parameters[0] as string, parameters[1] as string);
                case "getfulladdress":
                    return GetFullAddress(parameters[0] as string, parameters[1] as string, parameters[2] as string, parameters[3] as string);
                default:
                    return new ArgumentException("function name specified is invalid");
            }
        }

        /// ******************************************************************************
        /// <summary>
        /// A sample UDF definition. A User Defined Function in NosDB can be defined through implementing IUserDefinedFunctionProvider
        /// interface. This method concatenates employee first name and last name
        /// 
        /// Requirements:
        ///     1. A NosDB database and collection
        ///     2. This assembly to be built and deployed on NosDB server
        /// </summary>
        /// 
        /// <returns></returns>
        public static string GetFullName(string firstName, string lastName)
        {
            return firstName + " " + lastName;
        }

        /// ******************************************************************************
        /// <summary>
        /// A sample UDF definition. A User Defined Function in NosDB can be defined through implementing IUserDefinedFunctionProvider
        /// interface. This method concatenates employee address, city, region and country
        /// 
        /// Requirements:
        ///     1. A NosDB database and collection
        ///     2. This assembly to be built and deployed on NosDB server
        /// </summary>
        /// 
        /// <returns></returns>
        public static string GetFullAddress(string address, string city, string region, string country)
        {
            string fullAddress = "";
            if (!string.IsNullOrEmpty(address))
                fullAddress += address;
            if (!string.IsNullOrEmpty(city))
            {
                fullAddress += ", " + city;
            }
            if (!string.IsNullOrEmpty(region))
            {
                fullAddress += ", " + region;
            }
            if (!string.IsNullOrEmpty(country))
                fullAddress += ", " + country;

            return fullAddress;
        }
    }
}
