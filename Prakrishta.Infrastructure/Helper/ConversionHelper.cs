//----------------------------------------------------------------------------------
// <copyright file="ConversionHelper.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>6/26/2019</date>
// <summary>The Conversion Helper class</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Infrastructure.Helper
{
    using Prakrishta.Infrastructure.Extensions;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Defines the <see cref="ConversionHelper" /> class methods
    /// </summary>
    public class ConversionHelper
    {
        #region |Methods|

        /// <summary>
        /// The method to convert string value to decimal value
        /// </summary>
        /// <param name="value">The string value</param>
        /// <returns>The converted decimal value</returns>
        public static decimal GetDecimal(string value)
        {
            if (!string.IsNullOrEmpty(value) && ValidateSingleDecimal(value))
            {
                var nonNumeric = value.TrimNonNumericalsForDecimals();
                return nonNumeric.GetValue<decimal>(0.0M);
            }

            return 0.0M;
        }

        /// <summary>
        /// The method to convert string value to double value
        /// </summary>
        /// <param name="value">The string value</param>
        /// <returns>The converted double value</returns>
        public static double GetDouble(string value)
        {
            if (!string.IsNullOrEmpty(value) && ValidateSingleDecimal(value))
            {
                var nonNumeric = value.TrimNonNumericalsForDecimals();
                return nonNumeric.GetValue<double>(0.0);
            }

            return 0.0;
        }

        /// <summary>
        /// The method to convert string value to float value
        /// </summary>
        /// <param name="value">The string value</param>
        /// <returns>The converted float value</returns>
        public static float GetFloat(string value)
        {
            if (!string.IsNullOrEmpty(value) && ValidateSingleDecimal(value))
            {
                var nonNumeric = value.TrimNonNumericalsForDecimals();
                return nonNumeric.GetValue<float>(0.0f);
            }

            return 0.0f;
        }

        /// <summary>
        /// The method to convert string value to int value
        /// </summary>
        /// <param name="value">The string value</param>
        /// <returns>The converted integer value</returns>
        public static int GetInt(string value)
        {
            var nonNumeric = value.TrimNonNumericalsForDecimals();

            if (!string.IsNullOrEmpty(nonNumeric) && ValidateSingleDecimal(nonNumeric))
            {
                var lastIndex = nonNumeric.LastIndexOf('.');
                if (lastIndex > 0)
                {
                    nonNumeric = nonNumeric.Substring(0, lastIndex);
                }
            }

            return nonNumeric.GetValue<int>(0);
        }

        /// <summary>
        /// The method to convert string value to long value
        /// </summary>
        /// <param name="value">The string value</param>
        /// <returns>The converted long value</returns>
        public static long GetLong(string value)
        {
            var nonNumeric = value.TrimNonNumericalsForDecimals();

            if (!string.IsNullOrEmpty(nonNumeric) && ValidateSingleDecimal(nonNumeric))
            {
                var lastIndex = nonNumeric.LastIndexOf('.');
                if (lastIndex > 0)
                {
                    nonNumeric = nonNumeric.Substring(0, lastIndex);
                }
            }

            return nonNumeric.GetValue<long>(0);
        }

        /// <summary>
        /// The method to validate if the string has only one decimal point
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns>Returns true if only one period is present in the given string otherwise false</returns>
        public static bool ValidateSingleDecimal(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                var matches = Regex.Matches(value, @"[\.]");
                if (matches.Count > 1)
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        #endregion
    }
}
