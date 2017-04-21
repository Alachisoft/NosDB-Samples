﻿// ===============================================================================
// Alachisoft (R) NosDB Sample Code.
// ===============================================================================
// Copyright © Alachisoft.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================

namespace NosDB.Samples.NorthwindModels
{
    /// <summary>
    /// Model class for OrderDetails collection that is generated by using database first approach on a northwind database
    /// </summary>
    public partial class OrderDetails
    {
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
        public double? UnitPrice { get; set; }
        public int? Quantity { get; set; }
        public double? Discount { get; set; }

        public virtual Orders Order { get; set; }
    }
}
