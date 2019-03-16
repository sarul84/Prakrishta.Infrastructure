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
    using System.Reflection;

    /// <summary>
    /// Data Row extension class that has extension methods
    /// </summary>
    public static class DataRowExtensions
    {
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
    }
}
