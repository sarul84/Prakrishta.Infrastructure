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
        /// Get number of days between two dates
        /// </summary>
        /// <param name="currentDate">The start date</param>
        /// <param name="date">The end date</param>
        /// <returns>Number of days</returns>
        public static double NumberOfDays(this DateTime currentDate, DateTime date)
        {
            return currentDate.Subtract(date).TotalDays;
        }

        /// <summary>
        /// Get number of days between the date and today
        /// </summary>
        /// <param name="dateTime">The start date</param>
        /// <returns>Number of days</returns>
        public static double NumberOfDays(this DateTime dateTime)
        {
            return dateTime.Subtract(DateTime.Now).TotalDays;
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

        /// <summary>
        /// Sets the time of the current date with minute precision.
        /// </summary>
        /// <param name="current">The current date.</param>
        /// <param name="hour">The hour.</param>
        /// <returns>A DateTime.</returns>
        public static DateTime SetTime(this DateTime current, int hour)
        {
            return SetTime(current, hour, 0, 0, 0);
        }

        /// <summary>
        /// Sets the time of the current date with minute precision.
        /// </summary>
        /// <param name="current">The current date.</param>
        /// <param name="hour">The hour.</param>
        /// <param name="minute">The minute.</param>
        /// <returns>A DateTime.</returns>
        public static DateTime SetTime(this DateTime current, int hour, int minute)
        {
            return SetTime(current, hour, minute, 0, 0);
        }

        /// <summary>
        /// Sets the time of the current date with second precision.
        /// </summary>
        /// <param name="current">The current date.</param>
        /// <param name="hour">The hour.</param>
        /// <param name="minute">The minute.</param>
        /// <param name="second">The second.</param>
        /// <returns>A DateTime.</returns>
        public static DateTime SetTime(this DateTime current, int hour, int minute, int second)
        {
            return SetTime(current, hour, minute, second, 0);
        }

        /// <summary>
        /// Sets the time of the current date with millisecond precision.
        /// </summary>
        /// <param name="current">The current date.</param>
        /// <param name="hour">The hour.</param>
        /// <param name="minute">The minute.</param>
        /// <param name="second">The second.</param>
        /// <param name="millisecond">The millisecond.</param>
        /// <returns>A DateTime.</returns>
        public static DateTime SetTime(this DateTime current, int hour, int minute, int second, int millisecond)
        {
            return new DateTime(current.Year, current.Month, current.Day, hour, minute, second, millisecond);
        }
    }
}
