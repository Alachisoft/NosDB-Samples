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
    public class Category
    {
        [JsonProperty(PropertyName="CategoryID")]
        public long ID { get; set; }
        [JsonProperty(PropertyName = "CategoryName")]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
