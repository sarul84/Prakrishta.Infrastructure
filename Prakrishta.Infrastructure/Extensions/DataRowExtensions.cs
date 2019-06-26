//----------------------------------------------------------------------------------
// <copyright file="DataRowExtensions.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>3/16/2019</date>
// <summary>Class that has extension method of Data Row</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Infrastructure.Extensions
{
    using System;
    using System.Data;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Data Row extension class that has extension methods
    /// </summary>
    public static class DataRowExtensions
    {
        #region |Methods|

        /// <summary>
        /// The method to get value from data colum of the specific row
        /// </summary>
        /// <typeparam name="T">The generic type parameter</typeparam>
        /// <param name="row">The data row object</param>
        /// <param name="columnIndex">The columnIndex</param>
        /// <param name="defaultValue">The defaultValue for the type if no value found</param>
        /// <returns>The value of the <see cref="T"/></returns>
        public static T GetValue<T>(this DataRow row, int columnIndex, T defaultValue = default(T))
        {
            T result = defaultValue;
            if (row != null && columnIndex >= 0 && columnIndex < row.ItemArray.Count())
            {
                if (!row.IsNull(columnIndex))
                {
                    object value = row[columnIndex];
                    result = value.GetValue(defaultValue);
                }
            }

            return result;
        }

        /// <summary>
        /// The method to get value from data colum of the specific row
        /// </summary>
        /// <typeparam name="T">The generic type parameter</typeparam>
        /// <param name="row">The data row object</param>
        /// <param name="columnName">The column name</param>
        /// <param name="defaultValue">The defaultValue for the type if no value found</param>
        /// <returns>The value of the <see cref="T"/></returns>
        public static T GetValue<T>(this DataRow row, string columnName, T defaultValue = default(T))
        {
            T result = defaultValue;
            if (row != null && !string.IsNullOrEmpty(columnName))
            {
                if (row.Table.Columns.Contains(columnName))
                {
                    if (!row.IsNull(columnName))
                    {
                        object value = row[columnName];
                        result = value.GetValue(defaultValue);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Converts each data row to typed entity
        /// </summary>
        /// <typeparam name="TEntity">The generic entity type parameter</typeparam>
        /// <param name="row">The data row object</param>
        /// <returns>The converted typed entity</returns>
        public static TEntity ToEntity<TEntity>(this DataRow row) where TEntity : class, new()
        {
            Type type = typeof(TEntity);
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);

            return row.ToEntity<TEntity>(properties, fields);
        }

        /// <summary>
        /// Converts each data row to typed entity
        /// </summary>
        /// <typeparam name="TEntity">The generic entity type parameter</typeparam>
        /// <param name="row">The data row object</param>
        /// <param name="properties">The properties list</param>
        /// <param name="fields">The fields list</param>
        /// <returns>The converted typed entity</returns>
        public static TEntity ToEntity<TEntity>(this DataRow row, PropertyInfo[] properties,
            FieldInfo[] fields) where TEntity : class, new()
        {
            var entity = new TEntity();

            foreach (PropertyInfo property in properties)
            {
                if (row.Table.Columns.Contains(property.Name))
                {
                    Type valueType = property.PropertyType;
                    property.SetValue(entity, row[property.Name].To(valueType), null);
                }
            }

            foreach (FieldInfo field in fields)
            {
                if (row.Table.Columns.Contains(field.Name))
                {
                    Type valueType = field.FieldType;
                    field.SetValue(entity, row[field.Name].To(valueType));
                }
            }

            return entity;
        }

        #endregion
    }
}
