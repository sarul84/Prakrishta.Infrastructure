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

        /// <summary>
        /// Updates value in dictionary for the specified key
        /// </summary>
        /// <typeparam name="TKey">The key type (determined from the dictionary)</typeparam>
        /// <typeparam name="TValue">Value type (determined from the dictionary)</typeparam>
        /// <param name="dictionary">The dictionary</param>
        /// <param name="key">The key value to find it in source dictionary</param>
        /// <param name="value">The value to be updated in dictionary</param>
        public static void Update<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            ArgumentNullException.ThrowIfNull(dictionary);
            dictionary[key] = value;
        }

        /// <summary>
        /// Updates value in dictionary for the specified key
        /// </summary>
        /// <typeparam name="TKey">The key type (determined from the dictionary)</typeparam>
        /// <typeparam name="TValue">Value type (determined from the dictionary)</typeparam>
        /// <param name="dictionary">The source dictionary</param>
        /// <param name="valuePair">The key value pair</param>
        public static void Update<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
            KeyValuePair<TKey, TValue> valuePair)
        {
            if (dictionary == null) throw new ArgumentNullException();
            dictionary[valuePair.Key] = valuePair.Value;
        }

        /// <summary>
        /// Delete value from dictionary if the key exists
        /// </summary>
        /// <typeparam name="TKey">The key type (determined from the dictionary)</typeparam>
        /// <typeparam name="TValue">Value type (determined from the dictionary)</typeparam>
        /// <param name="dictionary">The source dictionary</param>
        /// <param name="key">The key that has to be searched in dictionary</param>
        public static void DeleteIfKeyExists<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            if (dictionary == null) return;
            if (dictionary.ContainsKey(key))
            {
                dictionary.Remove(key);
            }
        }

        /// <summary>
        /// Delete the key from source if the corresponding key for the value exists
        /// </summary>
        /// <typeparam name="TKey">The key type (determined from the dictionary)</typeparam>
        /// <typeparam name="TValue">Value type (determined from the dictionary)</typeparam>
        /// <param name="dictionary">The source dictionary</param>
        /// <param name="value">The value to be searched in dictionary</param>
        public static void DeleteIfValueExists<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, 
            TValue value) where TValue : IComparable<TValue>
        {
            if (dictionary == null) return;

            if (!dictionary.ContainsValue(value)) return;

            TKey key = default(TKey);
            foreach(var pair in dictionary)
            {
                if(pair.Value.CompareTo(value) == 0)
                {
                    key = pair.Key;
                    break;
                }
            }

            dictionary.Remove(key);
        }

        /// <summary>
        /// Add key and value if the key is not there in dictionary
        /// </summary>
        /// <typeparam name="TKey">The key type (determined from the dictionary)</typeparam>
        /// <typeparam name="TValue">Value type (determined from the dictionary)</typeparam>
        /// <param name="dictionary">The source dictionary</param>
        /// <param name="key">The key value to find it in source dictionary</param>
        /// <param name="value">The value to be updated in dictionary</param>
        public static void AddIfNot<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            ArgumentNullException.ThrowIfNull(dictionary);
            if(!dictionary.ContainsKey(key))
            {
                dictionary.Add(key, value);
            }
        }

        /// <summary>
        /// Add key to dictionary if not exists otherwise update the dictionary
        /// </summary>
        /// <typeparam name="TKey">The key type (determined from the dictionary)</typeparam>
        /// <typeparam name="TValue">Value type (determined from the dictionary)</typeparam>
        /// <param name="dictionary">The source dictionary</param>
        /// <param name="key">The key value to find it in source dictionary</param>
        /// <param name="value">The value to be updated in dictionary</param>
        public static void AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            ArgumentNullException.ThrowIfNull(dictionary);
            if (!dictionary.ContainsKey(key))
            {
                dictionary.Add(key, value);
            }
            else
            {
                dictionary[key] = value;
            }
        }

        /// <summary>
        /// Add key to dictionary if not exists otherwise update the dictionary
        /// </summary>
        /// <typeparam name="TKey">The key type (determined from the dictionary)</typeparam>
        /// <typeparam name="TValue">Value type (determined from the dictionary)</typeparam>
        /// <param name="dictionary">The source dictionary</param>
        /// <param name="valuePair">The key value pair</param>
        public static void AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, 
            KeyValuePair<TKey,TValue> keyValue)
        {
            ArgumentNullException.ThrowIfNull(dictionary);
            if (!dictionary.ContainsKey(keyValue.Key))
            {
                dictionary.Add(keyValue.Key, keyValue.Value);
            }
            else
            {
                dictionary[keyValue.Key] = keyValue.Value;
            }
        }
    }
}
