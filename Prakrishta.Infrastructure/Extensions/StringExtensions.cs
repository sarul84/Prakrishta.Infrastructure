//----------------------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>2/9/2019</date>
// <summary>Extension method for string type</summary>
//-----------------------------------------------------------------------------------

using Newtonsoft.Json;
using Prakrishta.Infrastructure.Helper;
using System;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

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

        /// <summary>
        /// Converts string to title case string
        /// </summary>
        /// <param name="inputString">Input value</param>
        /// <returns>Title case string</returns>
        public static string ToTitleCase(this string inputString) => CultureInfo.InvariantCulture?.TextInfo?.ToTitleCase(inputString.ToLower());

        /// <summary>
        /// Converts string to its boolean equivalent
        /// </summary>
        /// <param name="value">string to convert</param>
        /// <returns>boolean equivalent</returns>
        /// <remarks>
        ///     <exception cref="ArgumentException">
        ///         thrown in the event no boolean equivalent found or an empty or whitespace
        ///         string is passed
        ///     </exception>
        /// </remarks>
        public static bool ToBoolean(this string value)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(nameof(value));
            }
            string val = value.ToLower().Trim();
            switch (val)
            {
                case "false":
                    return false;
                case "f":
                    return false;
                case "true":
                    return true;
                case "t":
                    return true;
                case "yes":
                    return true;
                case "no":
                    return false;
                case "y":
                    return true;
                case "n":
                    return false;
                default:
                    throw new ArgumentException("Invalid boolean");
            }
        }

        /// <summary>
        ///     Validate email address
        /// </summary>
        /// <param name="email">string email address</param>
        /// <returns>true or false if email if valid</returns>
        public static bool IsEmailAddress(this string email)
        {
            string pattern =
                "^[a-zA-Z][\\w\\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\\w\\.-]*[a-zA-Z0-9]\\.[a-zA-Z][a-zA-Z\\.]*[a-zA-Z]$";
            return Regex.Match(email, pattern).Success;
        }

        /// <summary>
        /// Validate zip code if it satisfies the pattern (xxxxx-xxxx)
        /// </summary>
        /// <param name="zipCode">string zip code</param>
        /// <returns>True if it matches the pattern otherwise false</returns>
        public static bool IsValidUSAZip(this string zipCode)
        {
            string pattern = "^[0-9]{5}(?:-[0-9]{4})?$";
            return Regex.Match(zipCode, pattern).Success;
        }

        /// <summary>
        /// Validate zip code if it satisfies the pattern (xxxxxx)
        /// </summary>
        /// <param name="postalCode">string zip code</param>
        /// <returns>True if it matches the pattern otherwise false</returns>
        public static bool IsValidIndianPostalCode(this string postalCode)
        {
            string pattern = @"^\d{6}(-\d{4})?$";
            return Regex.Match(postalCode, pattern).Success;
        }

        /// <summary>
        /// Validate zip code if it satisfies the pattern
        /// Canadian postal codes can't contain the letters D, F, I, O, Q, or U, and cannot start with W or Z
        /// </summary>
        /// <param name="postalCode">string zip code</param>
        /// <returns>True if it matches the pattern otherwise false</returns>
        public static bool IsValidCanadianPostalCode(this string postalCode)
        {
            string pattern = @"^([ABCEGHJKLMNPRSTVXY]\d[ABCEGHJKLMNPRSTVWXYZ])\ {0,1}(\d[ABCEGHJKLMNPRSTVWXYZ]\d)$";
            return Regex.Match(postalCode, pattern).Success;
        }

        /// <summary>
        /// Truncate the string for the given length
        /// </summary>
        /// <param name="input">string to be truncated</param>
        /// <param name="maxLength">number of characters to truncate</param>
        /// <returns>Truncated string</returns>
        public static string Truncate(this string input, int maxLength)
        {
            if (string.IsNullOrEmpty(input) || maxLength <= 0)
            {
                return string.Empty;
            }

            if (input.Length > maxLength)
            {
                return input.Substring(0, maxLength);
            }

            return input;
        }

        /// <summary>
        /// Converts a Json string to object of type T method applicable for multi hierarchy objects i.e
        /// having zero or many parent child relationships, Ignore loop references and do not serialize if cycles are detected.
        /// </summary>
        /// <typeparam name="T">object to convert to</typeparam>
        /// <param name="json">json</param>
        /// <returns>object</returns>
        public static T JsonToObject<T>(this string json)
        {
            var settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            return JsonConvert.DeserializeObject<T>(json, settings);
        }

        /// <summary>
        /// Checks if the String contains only Unicode letters, digits. null will return false. 
        /// An empty String ("") will return false.
        /// </summary>
        /// <param name="val">string to check if is Alpha or Numeric</param>
        /// <returns></returns>
        public static bool IsAlphaNumeric(this string val)
        {
            if (string.IsNullOrEmpty(val))
            {
                return false;
            }
            return val.Trim().Replace(" ", "").All(char.IsLetterOrDigit);
        }

        /// <summary>
        ///  Validates if a string is valid IPv4
        ///  Regular expression taken from <a href="http://regexlib.com/REDetails.aspx?regexp_id=2035">Regex reference</a>
        /// </summary>
        /// <param name="val">string IP address</param>
        /// <returns>true if string matches valid IP address else false</returns>
        public static bool IsValidIPv4(this string val)
        {
            if (string.IsNullOrEmpty(val))
            {
                return false;
            }
            return Regex.Match(val,
                @"(?:^|\s)([a-z]{3,6}(?=://))?(://)?((?:25[0-5]|2[0-4]\d|[01]?\d\d?)\.(?:25[0-5]|2[0-4]\d|[01]?\d\d?)\.(?:25[0-5]|2[0-4]\d|[01]?\d\d?)\.(?:25[0-5]|2[0-4]\d|[01]?\d\d?))(?::(\d{2,5}))?(?:\s|$)")
                .Success;
        }

        /// <summary>
        /// Extracts the left part of the input string limited with the length parameter
        /// </summary>
        /// <param name="val">The input string to take the left part from</param>
        /// <param name="length">The total number characters to take from the input string</param>
        /// <returns>The substring starting at startIndex 0 until length</returns>
        /// <exception cref="System.ArgumentNullException">input is null</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Length is smaller than zero or higher than the length of input</exception>
        public static string Left(this string val, int length)
        {
            if (string.IsNullOrEmpty(val))
            {
                throw new ArgumentNullException(nameof(val));
            }
            if (length < 0 || length > val.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(length),
                    "length cannot be higher than total string length or less than 0");
            }
            return val.Substring(0, length);
        }

        /// <summary>
        /// Extracts the right part of the input string limited with the length parameter
        /// </summary>
        /// <param name="val">The input string to take the right part from</param>
        /// <param name="length">The total number characters to take from the input string</param>
        /// <returns>The substring taken from the input string</returns>
        /// <exception cref="System.ArgumentNullException">input is null</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Length is smaller than zero or higher than the length of input</exception>
        public static string Right(this string val, int length)
        {
            if (string.IsNullOrEmpty(val))
            {
                throw new ArgumentNullException(nameof(val));
            }
            if (length < 0 || length > val.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(length),
                    "length cannot be higher than total string length or less than 0");
            }
            return val.Substring(val.Length - length);
        }

        /// <summary>
        /// Get only digits from string if anything
        /// </summary>
        /// <param name="value">The original string value</param>
        /// <returns>The numeric value</returns>
        public static string Digits(this string value)
        {
            return new string(value?.Where(c => char.IsDigit(c)).ToArray());
        }

        /// <summary>
        /// Get has algorithm for the given input string
        /// </summary>
        /// <param name="input">The input string</param>
        /// <returns>The encrypted string</returns>
        public static string GetHashAlgorithm(this string input)
        {
            HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider();
            byte[] byteValue = Encoding.UTF8.GetBytes(input);
            byte[] byteHash = hashAlgorithm.ComputeHash(byteValue);
            return Convert.ToBase64String(byteHash);
        }
    }
}
