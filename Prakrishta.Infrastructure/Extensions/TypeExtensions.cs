//----------------------------------------------------------------------------------
// <copyright file="TypeExtensions.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>5/26/2019</date>
// <summary>Extension class for System.Type</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Infrastructure.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines the extension methods for <see cref="System.Type" /> class
    /// </summary>
    public static class TypeExtensions
    {
        #region |Methods|

        /// <summary>
        /// The method gets the class that is inherited from the specified interface
        /// </summary>
        /// <typeparam name="TInterface">The interface type</typeparam>
        /// <param name="types">The types<see cref="IEnumerable{Type}"/></param>
        /// <returns>The type that is derived from specific interface</returns>
        public static Type FilterClassByInterface<TInterface>(IEnumerable<Type> types)
        {
            return FilterClassesByInterface<TInterface>(types)?.FirstOrDefault();
        }

        /// <summary>
        /// The method gets the list of classes that are inherited from the specified interface
        /// </summary>
        /// <typeparam name="TInterface">The interface type</typeparam>
        /// <param name="types">The types<see cref="IEnumerable{Type}"/></param>
        /// <returns>The collection of types derived from specific interface</returns>
        public static IEnumerable<Type> FilterClassesByInterface<TInterface>(this IEnumerable<Type> types)
        {
            return types.Where(x => typeof(TInterface).IsAssignableFrom(x) && !x.IsInterface
                    && !x.IsAbstract);
        }

        /// <summary>
        /// The method gets the list of classes name that are inherited from the specified interface
        /// </summary>
        /// <typeparam name="TInterface">The interface type</typeparam>
        /// <param name="types">The types<see cref="IEnumerable{Type}"/></param>
        /// <returns>The collection of types' name that are derived from specific interface</returns>
        public static IEnumerable<string> GetClassNamesByInterface<TInterface>(this IEnumerable<Type> types)
        {
            return FilterClassesByInterface<TInterface>(types)?.Select(x => x.Name).ToList();
        }

        /// <summary>
        /// The type is still class but not array or generic list or string
        /// </summary>
        /// <param name="type">The type</param>
        /// <returns>The <see cref="bool"/></returns>
        public static bool IsCustomClass(this Type type)
        {
            if (type != null)
            {
                if (!type.IsArray && !type.IsGenericList() && !type.IsString())
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// The method checks if the type is GenericList and returns true if it is
        /// </summary>
        /// <param name="type">The type</param>
        /// <returns>True if it is GenericList type otherwise false</returns>
        public static bool IsGenericList(this Type type)
        {
            if (type != null)
            {
                if (type.IsGenericType)
                {
                    if (!type.ContainsGenericParameters)
                    {
                        type = type.GetGenericTypeDefinition();
                    }

                    var stringListType = typeof(List<string>);
                    var genericListType = stringListType.GetGenericTypeDefinition();

                    return type == genericListType;
                }
            }

            return false;
        }

        /// <summary>
        /// The method checks if the type is NullableType and returns true if it is
        /// </summary>
        /// <param name="type">The type</param>
        /// <returns>True if it is nullable type otherwise false</returns>
        public static bool IsNullableType(this Type type)
        {
            if (type != null)
            {
                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// The method checks if the type is string type and returns true if it is
        /// </summary>
        /// <param name="type">The type<see cref="Type"/></param>
        /// <returns>True if it is string type otherwise false</returns>
        public static bool IsString(this Type type)
        {
            return Type.GetTypeCode(type) == TypeCode.String;
        }

        #endregion
    }
}
