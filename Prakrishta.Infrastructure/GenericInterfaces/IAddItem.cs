//----------------------------------------------------------------------------------
// <copyright file="ICreateItem.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>2/8/2019</date>
// <summary>Contract that defines methods to create an item</summary>
//-----------------------------------------------------------------------------------

using System.Threading;
using System.Threading.Tasks;

namespace Prakrishta.Infrastructure.GenericInterfaces
{
    /// <summary>
    /// Interface that has definitions to add or create an item / entity
    /// </summary>
    /// <typeparam name="TEntity">Entity type that has to be created</typeparam>
    public interface IAddItem<out TEntity> where TEntity : class
    {
        /// <summary>
        /// Adds a new item or entity
        /// </summary>
        /// <returns>Returns newly created entity or item</returns>
        TEntity Add();
    }

    /// <summary>
    /// Interface that has definitions to add or create an item / entity
    /// </summary>
    /// <typeparam name="TInput">Input entity type</typeparam>
    /// <typeparam name="TEntity">Entity type that has to be created</typeparam>
    public interface IAddItem<in TInput, out TEntity> 
        where TEntity : class
        where TInput : class
    {
        /// <summary>
        /// Adds a new item or entity
        /// </summary>
        /// <param name="entity">Input entity object</param>
        /// <returns>Returns newly created entity or item</returns>
        TEntity Add(TInput entity);
    }    
}
