//----------------------------------------------------------------------------------
// <copyright file="IDynamicResolver.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>5/30/2019</date>
// <summary>Contract that defines methods to resolve dependencies dynamically</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Infrastructure.GenericInterfaces
{
    /// <summary>
    /// Defines the contract to resolve services dynamically
    /// </summary>
    /// <typeparam name="TInterface">The generic interface type to be resolved</typeparam>
    /// <typeparam name="TArg">The generic input type</typeparam>
    interface IDynamicResolver<out TInterface, in TArg>
    {
        /// <summary>
        /// Method to get the resolved service by key
        /// </summary>
        /// <param name="key">The key to get service</param>
        /// <returns>The resolved service</returns>
        TInterface GetService(TArg key);
    }
}
