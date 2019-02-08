//----------------------------------------------------------------------------------
// <copyright file="IAddCollection.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>2/8/2019</date>
// <summary>Contract that defines methods to item collection</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Infrastructure.GenericInterfaces
{
    using System.Collections.Generic;

    /// <summary>
    /// Interface that has definitions to add collection of items
    /// </summary>
    /// <typeparam name="TEntity">Entity type that needs to be added</typeparam>
    public interface IAddCollection<TEntity> where TEntity : class
    {
        /// <summary>
        ///  Add collection of items
        /// </summary>
        /// <param name="entities">Entity collection to be added</param>
        void AddRange(IEnumerable<TEntity> entities);
    }
}
