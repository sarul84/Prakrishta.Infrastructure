//----------------------------------------------------------------------------------
// <copyright file="CollectionExtensions.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>3/3/2019</date>
// <summary>The class that extends the functionality of Collection<T>.cs</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Infrastructure.Extensions
{
    using Prakrishta.Infrastructure.Helper;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    /// <summary>
    /// Class that has Extension methods for ICollection<T>
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Add record to the collection if not there in the collection
        /// </summary>
        /// <typeparam name="T">The generic type</typeparam>
        /// <param name="source">The original collection</param>
        /// <param name="value">The item to be added if not there</param>
        /// <param name="comparer">The equality comparer</param>
        /// <returns>True if record added otherwise false</returns>
        public static bool AddIfNot<T>(this ICollection<T> source, T @value, 
            IEqualityComparer<T> comparer = null)
        {
            if(!source.Contains(value, comparer))
            {
                source.Add(value);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Check if the collection is empty or not
        /// </summary>
        /// <typeparam name="T">The generic type</typeparam>
        /// <param name="source">The original collection</param>
        /// <returns>True if collection is empty otherwise false</returns>
        public static bool IsEmpty<T>(this ICollection<T> source) => source?.Any() == false;

        /// <summary>
        /// check if collection is empty or null
        /// </summary>
        /// <typeparam name="T">The generic type</typeparam>
        /// <param name="source">The original collection</param>
        /// <returns>True if collection is empty or null otherwise false</returns>
        public static bool IsNullOrEmpty<T>(this ICollection<T> source)
        {
            return (source == null || source?.Any() == false);
        }

        /// <summary>
        /// Check if any one of the item present in the collection
        /// </summary>
        /// <typeparam name="T">The generic type parameter</typeparam>
        /// <param name="source">The original collection source</param>
        /// <param name="comparer">The equality comparer</param>
        /// <param name="values">The items to be searched in collection</param>
        /// <returns>True if any one item present otherwise false</returns>
        public static bool ContainsAny<T>(this ICollection<T> source, 
            IEqualityComparer<T> comparer = null, params T[] values)
        {
            return values.Select(x => x)
                .Intersect(source, comparer)
                .Any();
        }

        /// <summary>
        /// Check if all the given records present in the collection
        /// </summary>
        /// <typeparam name="T">The generic type parameter</typeparam>
        /// <param name="source">The original collection source</param>
        /// <param name="comparer">The equality comparer</param>
        /// <param name="values">The items to be searched in collection</param>
        /// <returns>True if all items present otherwise false</returns>
        public static bool ContainsAll<T>(this ICollection<T> source, IEqualityComparer<T> comparer = null,
            params T[] values)
        {
            return values.Select(x => x)
                .Intersect(source, comparer)
                .Count() == values.Count();
        }

        /// <summary>
        /// Get distinct records by properties
        /// </summary>
        /// <typeparam name="T">The generic type parameter</typeparam>
        /// <param name="source">The collection source</param>
        /// <param name="equals">The equal delegate</param>
        /// <param name="getHashCode">Delegate to get hashcode</param>
        /// <returns>Distinct records</returns>
        public static IEnumerable<T> DistinctBy<T>(this ICollection<T> source,
            Func<T, T, bool> equals, Func<T, int> getHashCode)
        {
            return source.Distinct<T>(new DelegateComparer<T>(equals, getHashCode));
        }

        /// <summary>
        /// Get distinct records by property
        /// </summary>
        /// <typeparam name="T">The generic type parameter</typeparam>
        /// <param name="source">The collection source</param>
        /// <param name="propertyName">The property name that is going to be used for comparison</param>
        /// <returns>Distinct records</returns>
        public static IEnumerable<T> DistinctBy<T>(this ICollection<T> source, string propertyName)
        {
            return source.Distinct<T>(new PropertyComparer<T>(propertyName));
        }

        /// <summary>
        /// Remove items that matches the filter condition 
        /// </summary>
        /// <typeparam name="T">The generic type parameter</typeparam>
        /// <param name="source">The collection source</param>
        /// <param name="where">The filter condition</param>
        public static void Remove<T>(this ICollection<T> source, Func<T, bool> where)
        {
            var items = source.Where(where).ToArray();
            foreach(T item in items)
            {
                source.Remove(item);
            }
        }

        /// <summary>
        /// Remove item if that is there in collection
        /// </summary>
        /// <typeparam name="T">The generic type parameter</typeparam>
        /// <param name="source">The collection source</param>
        /// <param name="value">The item to be removed</param>
        /// <param name="comparer">The equality comparer</param>
        public static void Remove<T>(this ICollection<T> source, T @value, IEqualityComparer<T> comparer)
        {
            if(source.Contains(value, comparer))
            {
                source.Remove(value);
            }
        }

        /// <summary>
        /// Creates empty collection for the specified type
        /// </summary>
        /// <typeparam name="T">The generic type parameter</typeparam>
        /// <param name="source">The collection source</param>
        /// <returns>The new empty collection</returns>
        public static ICollection<T> Empty<T>(this ICollection<T> source) => new Collection<T>();

        /// <summary>
        /// An Collection<T>; extension method that switches item only when it exists in a collection.
        /// </summary>
        /// <typeparam name="T">The generic type</typeparam>
        /// <param name="source">The original list</param>
        /// <param name="oldValue">The old value that has be replaced</param>
        /// <param name="newValue">The new value</param>
        public static void Switch<T>(this Collection<T> source, T oldValue, T newValue)
        {
            var oldIndex = source.IndexOf(oldValue);
            while (oldIndex > 0)
            {
                source.RemoveAt(oldIndex);
                source.Insert(oldIndex, newValue);
                oldIndex = source.IndexOf(oldValue);
            }
        }

        /// <summary>
        /// Get random element from collection
        /// </summary>
        /// <typeparam name="T">The generic type parameter</typeparam>
        /// <param name="source">The original source</param>
        /// <returns>The random object</returns>
        public static T GetRandElement<T>(this ICollection<T> source)
        {
            return source.ElementAt(new Random().Next(source.Count));
        }
    }
}
