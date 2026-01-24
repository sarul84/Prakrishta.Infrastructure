//----------------------------------------------------------------------------------
// <copyright file="DataTableExtensions.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>3/3/2019</date>
// <summary>The extension class for DataTableExtensions.cs</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Infrastructure.Extensions
{
    using Prakrishta.Infrastructure.Helper;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Class that has extension method for DataTable
    /// </summary>
    public static class DataTableExtensions
    {
        /// <summary>
        /// Converts each data row in Data Table to typed entity
        /// </summary>
        /// <typeparam name="TEntity">The generic entity type parameter</typeparam>
        /// <param name="data">The datatable data</param>
        /// <returns>The converted typed entity collection</returns>
        public static IEnumerable<TEntity> ToEntities<TEntity>(this DataTable data) where TEntity : class, new()
        {
            Type type = typeof(TEntity);
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);

            var list = new Collection<TEntity>();

            foreach (DataRow dr in data.Rows)
            {
                TEntity entity = dr.ToEntity<TEntity>(properties, fields);
                list.Add(entity);
            }

            return list;
        }

        /// <summary>
        /// Check if two data table schema matches or not
        /// </summary>
        /// <param name="source">The source data table object</param>
        /// <param name="target">The target data table object</param>
        /// <returns>True if schema equals otherwise false</returns>
        public static bool IsSchemaEquals(this DataTable source, DataTable target)
        {
            if (source.Columns.Count != target.Columns.Count)
                return false;

            for (int i = 0; i < source.Columns.Count; i++)
            {
                var s = source.Columns[i];
                var t = target.Columns[i];

                if (!string.Equals(s.ColumnName, t.ColumnName, StringComparison.OrdinalIgnoreCase))
                    return false;

                if (s.DataType != t.DataType)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Compare two data table data and returns list of different records 
        /// </summary>
        /// <param name="data1">The data source table one</param>
        /// <param name="data2">The second data table source</param>
        /// <returns>Data table that has different records</returns>
        public static DataTable GetDataDifference(this DataTable data1, DataTable data2)
        {
            if (!data1.IsSchemaEquals(data2))
                throw new Exception("The schema of the two tables is not matching");

            DataTable result = data1.Clone(); // copies schema safely

            // Pre-size for performance
            var capacity = Math.Max(data1.Rows.Count, data2.Rows.Count);

            var data1Hashes = new Dictionary<int, List<DataRow>>(capacity);
            var data2Hashes = new Dictionary<int, List<DataRow>>(capacity);

            // Compute row hashes
            foreach (DataRow row in data1.Rows)
            {
                int hash = ComputeRowHash(row);
                if (!data1Hashes.TryGetValue(hash, out var list))
                    data1Hashes[hash] = list = new List<DataRow>();

                list.Add(row);
            }

            foreach (DataRow row in data2.Rows)
            {
                int hash = ComputeRowHash(row);
                if (!data2Hashes.TryGetValue(hash, out var list))
                    data2Hashes[hash] = list = new List<DataRow>();

                list.Add(row);
            }

            // Rows in data1 not in data2
            foreach (var kvp in data1Hashes)
            {
                if (!data2Hashes.ContainsKey(kvp.Key))
                {
                    foreach (var row in kvp.Value)
                        result.Rows.Add(row.ItemArray);
                }
            }

            // Rows in data2 not in data1
            foreach (var kvp in data2Hashes)
            {
                if (!data1Hashes.ContainsKey(kvp.Key))
                {
                    foreach (var row in kvp.Value)
                        result.Rows.Add(row.ItemArray);
                }
            }

            return result;
        }

        /// <summary>
        /// Computes a hash code for the specified DataRow based on the string representations of its column values.
        /// </summary>
        /// <remarks>The hash code is calculated using the trimmed string representations of each column
        /// value, compared using ordinal string comparison. Null values are treated as empty strings.</remarks>
        /// <param name="row">The DataRow whose column values are used to compute the hash code. Cannot be null.</param>
        /// <returns>An integer hash code representing the combined values of the DataRow's columns.</returns>
        private static int ComputeRowHash(DataRow row)
        {
            var hash = new HashCode();

            foreach (var item in row.ItemArray)
            {
                hash.Add(item?.ToString()?.Trim() ?? string.Empty, StringComparer.Ordinal);
            }

            return hash.ToHashCode();
        }


        /// <summary>
        /// Get list of columns from Data Column collection
        /// </summary>
        /// <param name="columnCollection">The data column collection object</param>
        /// <returns>Collection of data columns</returns>
        public static IEnumerable<DataColumn> AsEnumerable(this DataColumnCollection columnCollection)
        {
            return columnCollection.Cast<DataColumn>();
        }

        /// <summary>
        /// Get list of columns from the data table
        /// </summary>
        /// <param name="dataTable">The data table object</param>
        /// <returns>Collection of data columns</returns>
        public static IEnumerable<DataColumn> GetColumns(this DataTable dataTable)
        {
            return dataTable.Columns.AsEnumerable();
        }
    }
}
