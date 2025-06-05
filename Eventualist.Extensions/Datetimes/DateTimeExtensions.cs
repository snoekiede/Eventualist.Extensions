using System;
using System.Globalization;

namespace Eventualist.Extensions.Datetimes
{
    /// <summary>
    /// Extension methods for DateTime operations
    /// </summary>
    public static class DateTimeExtensions
    {
        #region Formatting Methods

        /// <summary>
        /// Formats date for the datepicker in yyyy/MM/dd format
        /// </summary>
        /// <param name="date">The underlying date</param>
        /// <param name="culture">Optional culture for formatting (uses current culture if not specified)</param>
        /// <returns>A formatted string</returns>
        public static string FormatDateForPicker(this DateTime date, CultureInfo? culture = null)
        {
            return date.ToString("yyyy/MM/dd", culture ?? CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Formats time for the datepicker in HH:mm format
        /// </summary>
        /// <param name="date">The underlying date</param>
        /// <param name="culture">Optional culture for formatting (uses current culture if not specified)</param>
        /// <returns>A formatted string</returns>
        public static string FormatTimeForPicker(this DateTime date, CultureInfo? culture = null)
        {
            return date.ToString("HH:mm", culture ?? CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Formats date and time for the datepicker in yyyy/MM/dd HH:mm format
        /// </summary>
        /// <param name="date">The underlying date</param>
        /// <param name="culture">Optional culture for formatting (uses current culture if not specified)</param>
        /// <returns>A formatted string</returns>
        public static string FormatDateTimeForPicker(this DateTime date, CultureInfo? culture = null)
        {
            return date.ToString("yyyy/MM/dd HH:mm", culture ?? CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Formats date for the datepicker with a custom format
        /// </summary>
        /// <param name="date">The underlying date</param>
        /// <param name="format">The custom format string</param>
        /// <param name="culture">Optional culture for formatting (uses current culture if not specified)</param>
        /// <returns>A formatted string</returns>
        public static string FormatForPicker(this DateTime date, string format, CultureInfo? culture = null)
        {
            ArgumentNullException.ThrowIfNull(format);
            return date.ToString(format, culture ?? CultureInfo.CurrentCulture);
        }

        #endregion

        #region Nullable DateTime Extensions

        /// <summary>
        /// Formats date for the datepicker or returns a default value if the date is null
        /// </summary>
        /// <param name="date">The nullable date</param>
        /// <param name="defaultValue">The default value to return if date is null</param>
        /// <returns>A formatted string or the default value</returns>
        public static string FormatDateForPicker(this DateTime? date, string defaultValue = "")
        {
            return date.HasValue ? date.Value.FormatDateForPicker() : defaultValue;
        }

        /// <summary>
        /// Formats time for the datepicker or returns a default value if the date is null
        /// </summary>
        /// <param name="date">The nullable date</param>
        /// <param name="defaultValue">The default value to return if date is null</param>
        /// <returns>A formatted string or the default value</returns>
        public static string FormatTimeForPicker(this DateTime? date, string defaultValue = "")
        {
            return date.HasValue ? date.Value.FormatTimeForPicker() : defaultValue;
        }

        /// <summary>
        /// Formats date and time for the datepicker or returns a default value if the date is null
        /// </summary>
        /// <param name="date">The nullable date</param>
        /// <param name="defaultValue">The default value to return if date is null</param>
        /// <returns>A formatted string or the default value</returns>
        public static string FormatDateTimeForPicker(this DateTime? date, string defaultValue = "")
        {
            return date.HasValue ? date.Value.FormatDateTimeForPicker() : defaultValue;
        }

        #endregion

        #region Date Utility Methods

        /// <summary>
        /// Returns a new DateTime with the time component set to the start of the day (00:00:00)
        /// </summary>
        /// <param name="date">The source date</param>
        /// <returns>A DateTime set to the start of the day</returns>
        public static DateTime StartOfDay(this DateTime date)
        {
            return date.Date;
        }

        /// <summary>
        /// Returns a new DateTime with the time component set to the end of the day (23:59:59.999)
        /// </summary>
        /// <param name="date">The source date</param>
        /// <returns>A DateTime set to the end of the day</returns>
        public static DateTime EndOfDay(this DateTime date)
        {
            return date.Date.AddDays(1).AddTicks(-1);
        }

        /// <summary>
        /// Returns a new DateTime set to the start of the month
        /// </summary>
        /// <param name="date">The source date</param>
        /// <returns>A DateTime set to the start of the month</returns>
        public static DateTime StartOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        /// <summary>
        /// Returns a new DateTime set to the end of the month
        /// </summary>
        /// <param name="date">The source date</param>
        /// <returns>A DateTime set to the end of the month</returns>
        public static DateTime EndOfMonth(this DateTime date)
        {
            return date.StartOfMonth().AddMonths(1).AddTicks(-1);
        }

        /// <summary>
        /// Determines whether the date falls on a weekend
        /// </summary>
        /// <param name="date">The date to check</param>
        /// <returns>True if the date is a weekend, otherwise false</returns>
        public static bool IsWeekend(this DateTime date)
        {
            return date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;
        }

        /// <summary>
        /// Adds only business days to the date (skipping weekends)
        /// </summary>
        /// <param name="date">The source date</param>
        /// <param name="days">The number of business days to add</param>
        /// <returns>A new DateTime with the business days added</returns>
        public static DateTime AddBusinessDays(this DateTime date, int days)
        {
            var result = date;
            var direction = days < 0 ? -1 : 1;
            
            for (int i = 0; i < Math.Abs(days); i++)
            {
                do
                {
                    result = result.AddDays(direction);
                } while (result.IsWeekend());
            }
            
            return result;
        }

        #endregion
    }
}
