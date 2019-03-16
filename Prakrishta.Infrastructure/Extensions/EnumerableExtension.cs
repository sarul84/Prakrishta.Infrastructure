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
    using System.ComponentModel;
    using System.Data;
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

        /// <summary>
        /// Converts List<string>() and make a delimited string
        /// </summary>
        /// <typeparam name="T">The generic type</typeparam>
        /// <param name="source">The original collection</param>
        /// <param name="itemOutput">The output filter expression</param>
        /// <param name="separator">The separator string</param>
        /// <returns>Delimited string</returns>
        public static string Join<T>(this IEnumerable<T> source, Func<T, string> itemOutput = null, string separator = ",")
        {
            itemOutput = itemOutput ?? (x => x.ToString());
            return string.Join(separator, source.Select(itemOutput).ToArray());
        }

        /// <summary>
        /// Converts Enumerable collection to hashset type
        /// </summary>
        /// <typeparam name="T">The generic type parameter</typeparam>
        /// <param name="source">The original collection source</param>
        /// <returns>The converted hashset object</returns>
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source)
        {
            return new HashSet<T>(source);
        }

        /// <summary>
        /// Convert collection to data table object
        /// </summary>
        /// <typeparam name="TEntity">The generic entity type</typeparam>
        /// <param name="source">The original collection</param>
        /// <returns>The data table object</returns>
        public static DataTable ToDataTable<TEntity>(this IEnumerable<TEntity> source)
        {
            var result = new DataTable();

            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(TEntity));
            
            foreach (PropertyDescriptor prop in properties)
            {
                result.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            foreach (TEntity item in source)
            {
                DataRow row = result.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                }

                result.Rows.Add(row);
            }           

            return result;
        }
    }
}
