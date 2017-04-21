// ===============================================================================
// Alachisoft (R) NosDB Sample Code.
// ===============================================================================
// Copyright © Alachisoft.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================

using Newtonsoft.Json;

namespace NosDB.Samples.EntityObjects
{
    public class Product
    {
        [JsonProperty(PropertyName = "ProductID")]
        public long ID { get; set; }

        public string ProductName { get; set; }
        public string QuantityPerUnit { get; set; }
        public double UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public int UnitsOnOrder { get; set; }
        public int ReorderLevel { get; set; }
        public bool Discontinued { get; set; }

        public long SupplierID { get; set; }
        public EmbeddedCategory Category { get; set; }
    }

    public class EmbeddedCategory
    {
        [JsonProperty(PropertyName = "CategoryID")]
        public long CategoryID { get; set; }
        [JsonProperty(PropertyName = "CategoryName")]
        public string CategoryName { get; set; }
    }
}
