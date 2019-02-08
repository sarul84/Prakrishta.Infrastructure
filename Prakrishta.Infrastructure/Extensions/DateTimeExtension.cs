//----------------------------------------------------------------------------------
// <copyright file="DateTimeExtension.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>2/9/2019</date>
// <summary>Extension class that has extension methods for datetime</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Infrastructure.Extensions
{
    using System;

    public static class DateTimeExtension
    {
        /// <summary>
        ///  Helps to figure out if dateTime holds a date value that is a weekend.
        /// </summary>
        /// <param name="value">DateTime value</param>
        /// <returns>Indicates if day is weekend or not</returns>
        public static bool IsWeekend(this DateTime value)
        {
            return (value.DayOfWeek == DayOfWeek.Sunday || value.DayOfWeek == DayOfWeek.Saturday);
        }

        /// <summary>
        /// Get the actual age of a person
        /// </summary>
        /// <param name="dateOfBirth">Date of birth</param>
        /// <returns>Age</returns>
        public static int Age(this DateTime dateOfBirth)
        {
            if (DateTime.Today.Month < dateOfBirth.Month ||
            DateTime.Today.Month == dateOfBirth.Month &&
             DateTime.Today.Day < dateOfBirth.Day)
            {
                return DateTime.Today.Year - dateOfBirth.Year - 1;
            }
            else
            {
                return DateTime.Today.Year - dateOfBirth.Year;
            }
        }

        /// <summary>
        /// Checks if the date is between the two provided dates
        /// </summary>
        /// <param name="currentDate">Current date</param>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        /// <param name="compareTime">boolean indicates if time has to be included or not</param>
        /// <returns>Indicates if date is between the given two dates</returns>
        public static bool IsBetween(this DateTime currentDate, DateTime startDate, 
            DateTime endDate, bool compareTime = false)
        {
            return compareTime ?
               currentDate >= startDate && currentDate <= endDate :
               currentDate.Date >= startDate.Date && currentDate.Date <= endDate.Date;
        }

        /// <summary>
        /// Gets the last date of the month of the DateTime.
        /// </summary>
        /// <param name="dateTime">date value</param>
        /// <returns>Returns last day of the month</returns>
        public static DateTime GetLastDayOfMonth(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1).AddMonths(1).AddDays(-1);
        }
    }
}
