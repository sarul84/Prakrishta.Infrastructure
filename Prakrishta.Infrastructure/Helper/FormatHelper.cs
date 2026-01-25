//----------------------------------------------------------------------------------
// <copyright file="FormatHelper.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>6/26/2019</date>
// <summary>The Format Helper class</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Infrastructure.Helper
{
    using Prakrishta.Infrastructure.Extensions;
    using System;
    using System.Globalization;
    using static Prakrishta.Infrastructure.Helper.ConversionHelper;

    /// <summary>
    /// Defines the <see cref="FormatHelper" /> methods
    /// </summary>
    public class FormatHelper
    {
        #region |Methods|

        /// <summary>
        /// The MakePlural
        /// </summary>
        /// <typeparam name="T">The generic type parameter</typeparam>
        /// <param name="number">The number<see cref="T"/></param>
        /// <param name="upperCase">The upperCase<see cref="bool"/></param>
        /// <param name="suffix">The suffix<see cref="string"/></param>
        /// <returns>The <see cref="string"/></returns>
        public static string MakePlural<T>(T number, bool upperCase = false, string suffix = "s") where T : IComparable
        {
            string plural = string.Empty;
            if (!string.IsNullOrEmpty(suffix))
            {
                if (number.CompareTo(1) != 0)
                {
                    plural = upperCase ? suffix.ToUpper(CultureInfo.CurrentCulture) : suffix.ToUpper(CultureInfo.CurrentCulture).ToLower(CultureInfo.CurrentCulture);
                }
            }

            return plural;
        }

        /// <summary>
        /// The Phone Number formatter
        /// </summary>
        /// <param name="phone">The phone number string</param>
        /// <param name="formatter">The formatter string</param>
        /// <returns>The formatted phone number string</returns>
        public static string PhoneNumber(string phone, string formatter)
        {
            string formattedPhoneNumber = phone;
            string? trimmed = formattedPhoneNumber.GetNumericals();
            long numericPhoneNumber = 0;
            if (!string.IsNullOrEmpty(trimmed))
            {
                if (trimmed?.Length == 7)
                {
                    numericPhoneNumber = GetLong(trimmed);
                    formattedPhoneNumber = numericPhoneNumber.ToString("###-####", CultureInfo.CurrentCulture);
                }
                else
                {
                    if (trimmed?.Length == 10)
                    {
                        numericPhoneNumber = GetLong(trimmed);
                        if (!string.IsNullOrEmpty(formatter))
                        {
                            formattedPhoneNumber = numericPhoneNumber.ToString(formatter, CultureInfo.CurrentCulture);
                        }
                    }
                }
            }

            return formattedPhoneNumber;
        }

        #endregion
    }
}
