using System;
using System.Globalization;

namespace Eventualist.Extensions.Datetimes
{
    /// <summary>
    /// Extension methods for DateTime operations
    /// </summary>
    public static class DateTimeExtensions
    {
        extension(DateTime date)
        {
            /// <summary>
            /// Formats date for the datepicker in yyyy/MM/dd format
            /// </summary>
            /// <param name="culture">Optional culture for formatting (uses current culture if not specified)</param>
            /// <returns>A formatted string</returns>
            public string FormatDateForPicker(CultureInfo? culture = null)
            {
                return date.ToString("yyyy/MM/dd", culture ?? CultureInfo.CurrentCulture);
            }

            /// <summary>
            /// Formats time for the datepicker in HH:mm format
            /// </summary>
            /// <param name="culture">Optional culture for formatting (uses current culture if not specified)</param>
            /// <returns>A formatted string</returns>
            public string FormatTimeForPicker(CultureInfo? culture = null)
            {
                return date.ToString("HH:mm", culture ?? CultureInfo.CurrentCulture);
            }

            /// <summary>
            /// Formats date and time for the datepicker in yyyy/MM/dd HH:mm format
            /// </summary>
            /// <param name="culture">Optional culture for formatting (uses current culture if not specified)</param>
            /// <returns>A formatted string</returns>
            public string FormatDateTimeForPicker(CultureInfo? culture = null)
            {
                return date.ToString("yyyy/MM/dd HH:mm", culture ?? CultureInfo.CurrentCulture);
            }

            /// <summary>
            /// Formats date for the datepicker with a custom format
            /// </summary>
            /// <param name="format">The custom format string</param>
            /// <param name="culture">Optional culture for formatting (uses current culture if not specified)</param>
            /// <returns>A formatted string</returns>
            public string FormatForPicker(string format, CultureInfo? culture = null)
            {
                ArgumentNullException.ThrowIfNull(format);
                return date.ToString(format, culture ?? CultureInfo.CurrentCulture);
            }

            /// <summary>
            /// Returns a new DateTime with the time component set to the start of the day (00:00:00)
            /// </summary>
            /// <returns>A DateTime set to the start of the day</returns>
            public DateTime StartOfDay()
            {
                return date.Date;
            }

            /// <summary>
            /// Returns a new DateTime with the time component set to the end of the day (23:59:59.999)
            /// </summary>
            /// <returns>A DateTime set to the end of the day</returns>
            public DateTime EndOfDay()
            {
                return date.Date.AddDays(1).AddTicks(-1);
            }

            /// <summary>
            /// Returns a new DateTime set to the start of the month
            /// </summary>
            /// <returns>A DateTime set to the start of the month</returns>
            public DateTime StartOfMonth()
            {
                return new DateTime(date.Year, date.Month, 1);
            }

            /// <summary>
            /// Returns a new DateTime set to the end of the month
            /// </summary>
            /// <returns>A DateTime set to the end of the month</returns>
            public DateTime EndOfMonth()
            {
                return date.StartOfMonth().AddMonths(1).AddTicks(-1);
            }

            /// <summary>
            /// Determines whether the date falls on a weekend
            /// </summary>
            /// <returns>True if the date is a weekend, otherwise false</returns>
            public bool IsWeekend()
            {
                return date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;
            }

            /// <summary>
            /// Adds only business days to the date (skipping weekends)
            /// </summary>
            /// <param name="days">The number of business days to add</param>
            /// <returns>A new DateTime with the business days added</returns>
            public DateTime AddBusinessDays(int days)
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
        }



        extension(DateTime? date)
        {
            /// <summary>
            /// Formats date for the datepicker or returns a default value if the date is null
            /// </summary>
            /// <param name="defaultValue">The default value to return if date is null</param>
            /// <returns>A formatted string or the default value</returns>
            public string FormatDateForPicker(string defaultValue = "")
            {
                return date.HasValue ? date.Value.FormatDateForPicker() : defaultValue;
            }

            /// <summary>
            /// Formats time for the datepicker or returns a default value if the date is null
            /// </summary>
            /// <param name="defaultValue">The default value to return if date is null</param>
            /// <returns>A formatted string or the default value</returns>
            public string FormatTimeForPicker(string defaultValue = "")
            {
                return date.HasValue ? date.Value.FormatTimeForPicker() : defaultValue;
            }

            /// <summary>
            /// Formats date and time for the datepicker or returns a default value if the date is null
            /// </summary>
            /// <param name="defaultValue">The default value to return if date is null</param>
            /// <returns>A formatted string or the default value</returns>
            public string FormatDateTimeForPicker(string defaultValue = "")
            {
                return date.HasValue ? date.Value.FormatDateTimeForPicker() : defaultValue;
            }
        }

    }
}
