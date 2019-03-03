//----------------------------------------------------------------------------------
// <copyright file="ListExtensions.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>3/3/2019</date>
// <summary>Extension class to expand the functionality of List</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Infrastructure.Extensions
{
    using System.Collections.Generic;

    public static class ListExtensions
    {
        /// <summary>
        /// An IList<T>; extension method that switches item only when it exists in a collection.
        /// </summary>
        /// <typeparam name="T">The generic type</typeparam>
        /// <param name="list">The original list</param>
        /// <param name="oldValue">The old value that has be replaced</param>
        /// <param name="newValue">The new value</param>
        public static void Switch<T>(this IList<T> list, T oldValue, T newValue)
        {
            var oldIndex = list.IndexOf(oldValue);
            while (oldIndex > 0)
            {
                list.RemoveAt(oldIndex);
                list.Insert(oldIndex, newValue);
                oldIndex = list.IndexOf(oldValue);
            }
        }

        /// <summary>
        /// Creates empty list for the specified type
        /// </summary>
        /// <typeparam name="T">The generic type parameter</typeparam>
        /// <param name="source">The collection source</param>
        /// <returns>The new empty collection</returns>
        public static IList<T> Empty<T>(this IList<T> source) => new List<T>();
    }
}
