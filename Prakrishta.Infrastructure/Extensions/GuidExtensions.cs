//----------------------------------------------------------------------------------
// <copyright file="GuidExtensions.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>3/3/2019</date>
// <summary>GuidExtensions.cs</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Infrastructure.Extensions
{
    using System;

    public static class GuidExtensions
    {
        /// <summary>
        /// A GUID extension method that query if it is empty.
        /// </summary>
        /// <param name="value">The value to act on.</param>
        /// <returns>true if empty, false if not.</returns>
        public static bool IsEmpty(this Guid @value)
        {
            return value == Guid.Empty;
        }

        /// <summary>
        /// A GUID extension method that query if it is null or empty.
        /// </summary>
        /// <param name="value">The value to act on.</param>
        /// <returns>true if empty or null otherwise false.</returns>
        public static bool IsEmptyOrNull(this Guid? @value)
        {
            return value == null || value == Guid.Empty;
        }
    }
}
