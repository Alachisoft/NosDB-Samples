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
    class ReducerFactory : IReducerFactory
    {
        public IReducer Create(object key)
        {
            return new Reducer(key as string);
        }
    }

    /// <summary>
    /// A sample reducer implementation for a MapReduce task.
    /// </summary>
    [Serializable]
    public class Reducer : IReducer
    {
        private string key;
        private int count = 0;

        public Reducer(string key)
        {
            this.key = key;
        }

        public void BeginReduce()
        {
            // Do some initialization here
        }

        public object FinishReduce()
        {
            return new KeyValuePair(key, count);
        }

        public void Reduce(object value)
        {
            count += (int)value;
        }

        public void Dispose()
        {
            // Release resources
        }
    }
}
