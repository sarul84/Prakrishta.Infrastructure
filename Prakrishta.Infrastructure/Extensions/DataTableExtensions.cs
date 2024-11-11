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

            var sourceColumns = source.Columns.Cast<DataColumn>();
            var targetColumns = target.Columns.Cast<DataColumn>();

            var exceptCount = sourceColumns.Except(targetColumns, new DelegateComparer<DataColumn>((s, t) =>
                s.ColumnName == t.ColumnName && s.DataType == t.DataType,
                x =>
                {
                    var hash = 17;
                    hash = 31 * hash + x.ColumnName.GetHashCode();
                    hash = 31 * hash + x.DataType.GetHashCode();
                    return hash;
                })).Count();
            return (exceptCount == 0);
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
            {
                throw new Exception("The schema of the two tables is not matching");
            }

            DataTable result = new("Result");
            foreach (DataColumn column in data1.Columns)
            {
                result.Columns.Add(column.ColumnName, column.DataType);
            }

            // Create dictionaries to store hash codes of rows from data1 and data2
            Dictionary<string, DataRow> data1RowHashes = [];
            Dictionary<string, DataRow> data2RowHashes = [];

            // Populate the dictionaries
            foreach (DataRow row in data1.Rows)
            {
                data1RowHashes[string.Join(",", row.ItemArray)] = row;
            }

            foreach (DataRow row in data2.Rows)
            {
                data2RowHashes[string.Join(",", row.ItemArray)] = row;
            }

            // Find rows in data1 that are not in data2
            foreach (var entry in data1RowHashes)
            {
                if (!data2RowHashes.ContainsKey(entry.Key))
                {
                    result.ImportRow(entry.Value);
                }
            }

            // Find rows in data2 that are not in data1
            foreach (var entry in data2RowHashes)
            {
                if (!data1RowHashes.ContainsKey(entry.Key))
                {
                    result.ImportRow(entry.Value);
                }
            }

            return result;
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
