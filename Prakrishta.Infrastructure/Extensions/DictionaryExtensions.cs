//----------------------------------------------------------------------------------
// <copyright file="DictionaryExtensions.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>3/16/2019</date>
// <summary>Class that has extension methods of Dictionary type</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Infrastructure.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class DictionaryExtensions
    {
        /// <summary>
        /// Returns the key of the highest value in a dictionary.
        /// </summary>
        /// <typeparam name="TKey">The key type (determined from the dictionary)</typeparam>
        /// <typeparam name="TValue">Value type (determined from the dictionary), must implement IComparable<Value></typeparam>
        /// <param name="dictionary">The dictionary</param>
        /// <returns>The key of the highest value in the dic.</returns>
        public static TKey ArgMax<TKey, TValue>(this IDictionary<TKey, TValue> dictionary) where TValue : IComparable<TValue>
        {
            if (dictionary == null || dictionary.Count == 0) return default(TKey);
            var dicList = dictionary.ToList();
            var maxKvp = dicList.First();
            foreach (var kvp in dicList.Skip(1))
            {
                if (kvp.Value.CompareTo(maxKvp.Value) > 0)
                {
                    maxKvp = kvp;
                }
            }
            return maxKvp.Key;
        }
    }
}
