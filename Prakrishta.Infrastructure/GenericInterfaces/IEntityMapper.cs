//----------------------------------------------------------------------------------
// <copyright file="IEntityMapper.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>6/8/2019</date>
// <summary>Contract that defines methods for Entity Mapping</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Infrastructure.GenericInterfaces
{
    #region Interfaces

    /// <summary>
    /// Defines the methods for entity mapper
    /// </summary>
    /// <typeparam name="TSource">The generic source type</typeparam>
    public interface IEntityMapper<in TSource> where TSource : class
    {
        #region |Methods|

        /// <summary>
        /// The method to copy property values from source
        /// </summary>
        /// <param name="source">The source to be mapped</param>
        void CopyFrom(TSource source);

        #endregion
    }

    /// <summary>
    /// Defines the methods for entity mapper
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TDestination"></typeparam>
    public interface IEntityMapper<in TSource, out TDestination>
        where TSource : class
        where TDestination : class
    {
        #region |Methods|

        /// <summary>
        /// The method to copy property values from source to destination type
        /// </summary>
        /// <param name="source">The source to be mapped</param>
        /// <returns>The Destination type created from source</returns>
        TDestination CopyFrom(TSource source);

        #endregion
    }

    #endregion
}
