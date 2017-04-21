﻿// ===============================================================================
// Alachisoft (R) NosDB Sample Code.
// ===============================================================================
// Copyright © Alachisoft.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================

using System;
using System.Collections.Generic;

namespace NosDB.Samples.NorthwindModels
{
    /// <summary>
    /// Model class for Orders collection that is generated by using database first approach on a northwind database
    /// </summary>
    public partial class Orders
    {
        public Orders()
        {
            OrderDetails = new HashSet<OrderDetails>();
        }

        public int OrderId { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public int? ShipVia { get; set; }
        public double? Freight { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipPostalCode { get; set; }
        public string Shipcountry { get; set; }
        public string CustomerId { get; set; }
        public int? EmployeeId { get; set; }
        public string ShipRegion { get; set; }

        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}