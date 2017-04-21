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
    [Serializable]
    class CombinerFactory : ICombinerFactory
    {
        public ICombiner Create(object key)
        {
            return new Combiner();
        }
    }

    /// <summary>
    /// A sample combiner implementation for a MapReduce task 
    /// </summary>
    [Serializable]
    public class Combiner : ICombiner
    {
        private int count = 0;

        public void BeginCombine()
        {
            // Initializations here
        }

        public void Combine(object value)
        {
            count++;
        }

        public object FinishChunk()
        {
            return count;
        }

        public void Dispose()
        {
            // Release resources
        }
    }
}
