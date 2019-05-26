//----------------------------------------------------------------------------------
// <copyright file="IAsyncInitializer.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>5/26/2019</date>
// <summary>Contract that defines Async Initialization</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Infrastructure.AsyncInterfaces
{
    using System.Threading.Tasks;

    /// <summary>
    /// Marks a type as requiring asynchronous initialization and provides the result of that initialization.
    /// Credits to Stephen Cleary
    /// Reference Link: https://blog.stephencleary.com/2013/01/async-oop-2-constructors.html
    /// </summary>
    public interface IAsyncInitializer
    {
        /// <summary>
        /// Gets the result of the asynchronous initialization of this instance.
        /// </summary>
        Task Initialization { get; }
    }
}
