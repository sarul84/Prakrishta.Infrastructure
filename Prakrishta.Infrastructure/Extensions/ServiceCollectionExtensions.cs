//----------------------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>6/21/2019</date>
// <summary>The class that extends the functionality of IServiceCollection.cs</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Infrastructure.Extensions
{
    using System;
    using System.Reflection;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Defines the <see cref="ServiceCollectionExtensions" /> class
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// The method that adds all the classes inherited from the generic interface type as specific implemented interface
        /// </summary>
        /// <param name="services">The services collection object</param>
        /// <param name="assemblyString">The assembly name where the implementation resides</param>
        /// <param name="compareType">The generic interface type information</param>
        /// <param name="lifetime">The service lifetime information, default it will be scoped</param>
        public static void AddClassesAsImplementedInterface(
            this IServiceCollection services, 
            string assemblyString, 
            Type compareType,
            ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            AddClassesAsImplementedInterface(services, Assembly.Load(assemblyString), compareType, lifetime);
        }

        /// <summary>
        /// The method that adds all the classes inherited from the generic interface type as specific implemented interface
        /// </summary>
        /// <param name="services">The services collection object</param>
        /// <param name="assembly">The assembly that has implementations detail</param>
        /// <param name="compareType">The generic interface type information</param>
        /// <param name="lifetime">The service lifetime information, default it will be scoped</param>
        public static void AddClassesAsImplementedInterface(
            this IServiceCollection services, 
            Assembly assembly, 
            Type compareType,
            ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            assembly.GetTypesAssignableTo(compareType).ForEach((type) =>
            {
                type.ImplementedInterfaces.ForEach(implementedInterface =>
                {
                    switch (lifetime)
                    {
                        case ServiceLifetime.Scoped:
                            services.AddScoped(implementedInterface, type);
                            break;
                        case ServiceLifetime.Singleton:
                            services.AddSingleton(implementedInterface, type);
                            break;
                        case ServiceLifetime.Transient:
                            services.AddTransient(implementedInterface, type);
                            break;
                    }
                });
            });
        }

        /// <summary>
        /// The method that adds all the classes inherited from the generic interface type as specific implemented interface
        /// </summary>
        /// <param name="services">The services collection object</param>
        /// <param name="assemblyString">The assembly name where the implementation resides</param>
        /// <param name="compareType">The generic interface type information</param>
        /// <param name="action">The action<see cref="Action{Type, Type}"/> that has to be executed</param>
        public static void AddClassesAsImplementedInterface(this IServiceCollection services, string assemblyString, Type compareType, Action<Type, Type> action)
        {
            AddClassesAsImplementedInterface(services, Assembly.Load(assemblyString), compareType, action);
        }

        /// <summary>
        /// The method that adds all the classes inherited from the generic interface type as specific implemented interface
        /// </summary>
        /// <param name="services">The services collection object</param>
        /// <param name="assembly">The assembly that has implementations detail</param>
        /// <param name="compareType">The generic interface type information</param>
        /// <param name="action">The action<see cref="Action{Type, Type}"/> that has to be executed</param>
        public static void AddClassesAsImplementedInterface(this IServiceCollection services, Assembly assembly, Type compareType, Action<Type, Type> action)
        {
            assembly.GetTypesAssignableTo(compareType).ForEach((type) =>
            {
                type.ImplementedInterfaces.ForEach(implementedInterface =>
                {
                    action(implementedInterface, type);
                });
            });
        }
    }
}
