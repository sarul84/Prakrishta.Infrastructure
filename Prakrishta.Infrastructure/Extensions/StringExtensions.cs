//----------------------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>2/9/2019</date>
// <summary>Extension method for string type</summary>
//-----------------------------------------------------------------------------------

using Prakrishta.Infrastructure.Helper;

namespace Prakrishta.Infrastructure.Extensions
{
    /// <summary>
    /// Class that has extension methods for string type
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Parse string value to enum value
        /// </summary>
        /// <typeparam name="TEnum">Enum type to which the conversion should happen</typeparam>
        /// <param name="value">string value</param>
        /// <param name="ignoreCase">indicates if case insenstive</param>
        /// <returns>Enum value</returns>
        public static TEnum ToEnum<TEnum>(this string value, bool ignoreCase = false) 
            where TEnum : struct
        {
            return EnumHelper.Parse<TEnum>(value, ignoreCase);
        }
    }
}
