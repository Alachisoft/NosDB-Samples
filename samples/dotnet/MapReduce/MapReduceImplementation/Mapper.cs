// ===============================================================================
// Alachisoft (R) NosDB Sample Code.
// ===============================================================================
// Copyright © Alachisoft.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================

using NosDB.Common.MapReduce;
using System;

namespace NosDB.Samples.MapReduceImplementation
{
    /// <summary>
    /// A sample mapper implementation for a MapReduce task. 
    /// </summary>
    [Serializable]
    public class Mapper : IMapper
    {
        public void Map(NosDB.Common.IJSONDocument document, IOutputMap context)
        {
            if (document != null)
            {
                context.Emit(document["ShipCity"], 1);
            }
        }

        public void Dispose()
        {
            // Releasing resources (if used)
        }
    }
}