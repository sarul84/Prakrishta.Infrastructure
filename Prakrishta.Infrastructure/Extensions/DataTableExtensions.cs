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
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data;
    using System.Reflection;

    /// <summary>
    /// Class that has extension method for DataTable
    /// </summary>
    public static class DataTableExtensions
    {
        /// <summary>
        /// Converts each data row to typed entity
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
                var entity = new TEntity();

                foreach (PropertyInfo property in properties)
                {
                    if (data.Columns.Contains(property.Name))
                    {
                        Type valueType = property.PropertyType;
                        property.SetValue(entity, dr[property.Name].To(valueType), null);
                    }
                }

                foreach (FieldInfo field in fields)
                {
                    if (data.Columns.Contains(field.Name))
                    {
                        Type valueType = field.FieldType;
                        field.SetValue(entity, dr[field.Name].To(valueType));
                    }
                }

                list.Add(entity);
            }

            return list;
        }
    }
}
