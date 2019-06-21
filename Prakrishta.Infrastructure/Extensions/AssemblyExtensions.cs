//----------------------------------------------------------------------------------
// <copyright file="AssemblyExtensions.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>6/21/2019</date>
// <summary>The class that extends the functionality of Assembly.cs</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Infrastructure.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Reflection;
    using Microsoft.Extensions.DependencyInjection; 
    
    /// <summary>
    /// Defines the <see cref="ServiceCollectionExtensions" /> class
    /// </summary>
    public static class AssemblyExtensions
    {
        /// <summary>
        /// The GetTypesAssignableTo
        /// </summary>
        /// <param name="assembly">The assembly</param>
        /// <param name="compareType">The generic interface type information</param>
        /// <returns>The <see cref="List{TypeInfo}"/> that has implemented classes type info</returns>
        public static ICollection<TypeInfo> GetTypesAssignableTo(this Assembly assembly, Type compareType)
        {
            var typeInfoList = new Collection<TypeInfo>();
            foreach (var type in assembly.DefinedTypes)
            {
                bool isAssignableTo = type.GetInterfaces()
                                        .Any(i => i.IsGenericType
                                                && i.GetGenericTypeDefinition() == compareType);
                if (isAssignableTo && compareType != type)
                {
                    typeInfoList.Add(type);
                }
            }

            return typeInfoList;
        }
    }
}
