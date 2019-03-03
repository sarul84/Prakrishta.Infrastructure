//----------------------------------------------------------------------------------
// <copyright file="EnumerableExtension.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>3/3/2019</date>
// <summary>The extension class for IEnumerable type</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Infrastructure.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class EnumerableExtension
    {
        /// <summary>
        /// Enumerates for each in this collection.
        /// </summary>
        /// <typeparam name="T">The generic type parameter</typeparam>
        /// <param name="source">The collection source</param>
        /// <param name="action">The action to be performed</param>
        /// <returns>An enumerator that allows foreach to be used to process for each in this collection.</returns>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            T[] array = source.ToArray();
            foreach (T item in array)
            {
                action(item);
            }
            return array;
        }

        /// <summary>
        /// Enumerates for each in this collection.
        /// </summary>
        /// <typeparam name="T">The generic type parameter</typeparam>
        /// <param name="source">The collection source</param>
        /// <param name="action">The action to be performed</param>
        /// <returns>An enumerator that allows foreach to be used to process for each in this collection.</returns>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T, int> action)
        {
            T[] array = source.ToArray();

            for (int i = 0; i < array.Length; i++)
            {
                action(array[i], i);
            }

            return array;
        }

        /// <summary>
        /// Check if any one of the item present in the collection
        /// </summary>
        /// <typeparam name="T">The generic type parameter</typeparam>
        /// <param name="source">The collection source</param>
        /// <param name="comparer">The equality comparer</param>
        /// <param name="values">The items to be searched in collection</param>
        /// <returns>True if any one item present otherwise false</returns>
        public static bool ContainsAny<T>(this IEnumerable<T> source, 
            IEqualityComparer<T> comparer, params T[] values)
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
        public static bool ContainsAll<T>(this IEnumerable<T> source, IEqualityComparer<T> comparer, params T[] values)
        {
            return values.Select(x => x)
                .Intersect(source, comparer)
                .Count() == values.Count();
        }

        /// <summary>
        /// Check if the collection is empty or not
        /// </summary>
        /// <typeparam name="T">The generic type</typeparam>
        /// <param name="source">The original collection</param>
        /// <returns>True if collection is empty otherwise false</returns>
        public static bool IsEmpty<T>(this IEnumerable<T> source)
        {
            return source?.Count() == 0;
        }

        /// <summary>
        /// check if collection is empty or null
        /// </summary>
        /// <typeparam name="T">The generic type</typeparam>
        /// <param name="source">The original collection</param>
        /// <returns>True if collection is empty or null otherwise false</returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            return (source == null || source?.Count() == 0);
        }
    }
}
