//----------------------------------------------------------------------------------
// <copyright file="ValueTypeExtensions.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>6/26/2019</date>
// <summary>The extenstion class that has method definitions for ValueTypes</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Infrastructure.Extensions
{
    using Prakrishta.Infrastructure.Helper;
    using System;
    using System.Globalization;

    /// <summary>
    /// Defines the <see cref="ValueTypeExtensions" /> extension methods
    /// </summary>
    public static class ValueTypeExtensions
    {
        #region |Methods|

        /// <summary>
        /// The method that converts long type PhoneNumber into string formatted
        /// </summary>
        /// <param name="value">The phone number value</param>
        /// <param name="formatter">The formatter</param>
        /// <returns>The formatted phone number</returns>
        public static string PhoneNumber(this long value, string formatter) => FormatHelper.PhoneNumber(value.ToString(CultureInfo.CurrentCulture), formatter);

        /// <summary>
        /// The method converts decimal value into formatted Round Decimal string
        /// </summary>
        /// <param name="value">The decimal value</param>
        /// <param name="digits">The digits</param>
        /// <returns>The formatted Round Decimal string</returns>
        public static string RoundDecimal(this decimal value, int digits)
        {
            return string.Format(CultureInfo.CurrentCulture, "{0:F" + digits.ToString(CultureInfo.CurrentCulture) + "}", Math.Round(value, digits));
        }

        /// <summary>
        /// The method converts decimal value into formatted Round Decimal string
        /// </summary>
        /// <param name="value">The decimal value</param>
        /// <param name="digits">The digits</param>
        /// <param name="length">The length</param>
        /// <returns>The formatted Round Decimal string</returns>
        public static string RoundDecimal(this decimal value, int digits, int length)
        {
            return string.Format(CultureInfo.CurrentCulture, "{0," + length.ToString(CultureInfo.CurrentCulture) + ":f" + digits.ToString(CultureInfo.CurrentCulture) + "}", Math.Round(value, digits));
        }

        /// <summary>
        /// The method converts decimal value into formatted Round double string
        /// </summary>
        /// <param name="value">The value</param>
        /// <param name="digits">The digits</param>
        /// <returns>The formatted Round double string</returns>
        public static string RoundDouble(this double value, int digits)
        {
            return string.Format(CultureInfo.CurrentCulture, "{0:F" + digits.ToString(CultureInfo.CurrentCulture) + "}", Math.Round(value, digits));
        }

        /// <summary>
        /// The method converts decimal value into formatted Round double string
        /// </summary>
        /// <param name="value">The value</param>
        /// <param name="digits">The digits</param>
        /// <param name="length">The length</param>
        /// <returns>The formatted Round double string</returns>
        public static string RoundDouble(this double value, int digits, int length)
        {
            return string.Format(CultureInfo.CurrentCulture, "{0," + length.ToString(CultureInfo.CurrentCulture) + ":f" + digits.ToString(CultureInfo.CurrentCulture) + "}", Math.Round(value, digits));
        }

        /// <summary>
        /// The method converts decimal value into formatted currency string
        /// </summary>
        /// <param name="value">The currency value</param>
        /// <returns>The formatted currency string</returns>
        public static string ToCurrencyString(this decimal value)
        {
            return string.Format(CultureInfo.CurrentCulture, "{0:C2}", value);
        }

        /// <summary>
        /// The method converts double value into formatted currency string
        /// </summary>
        /// <param name="value">The currency value</param>
        /// <returns>The formatted currency string</returns>
        public static string ToCurrencyString(this double value)
        {
            return string.Format(CultureInfo.CurrentCulture, "{0:C2}", value);
        }

        #endregion
    }
}
