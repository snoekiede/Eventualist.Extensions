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

            /// <summary>
            /// Calculates the age in years from this date to the current date
            /// </summary>
            /// <returns>The age in years</returns>
            /// <example>
            /// <code>
            /// var birthdate = new DateTime(1990, 1, 1);
            /// var age = birthdate.Age(); // Returns the current age
            /// </code>
            /// </example>
            public int Age()
            {
                return Age(DateTime.Today);
            }

            /// <summary>
            /// Calculates the age in years from this date to a specific date
            /// </summary>
            /// <param name="asOfDate">The date to calculate the age as of</param>
            /// <returns>The age in years</returns>
            public int Age(DateTime asOfDate)
            {
                var age = asOfDate.Year - date.Year;
                
                // Subtract one year if birthday hasn't occurred yet this year
                if (asOfDate.Month < date.Month || 
                    (asOfDate.Month == date.Month && asOfDate.Day < date.Day))
                {
                    age--;
                }
                
                return age;
            }

            /// <summary>
            /// Returns a human-readable relative time string (e.g., "2 hours ago", "3 days ago")
            /// </summary>
            /// <returns>A relative time string</returns>
            /// <example>
            /// <code>
            /// var pastDate = DateTime.Now.AddHours(-2);
            /// pastDate.ToRelativeTime() // Returns "2 hours ago"
            /// </code>
            /// </example>
            public string ToRelativeTime()
            {
                return ToRelativeTime(DateTime.Now);
            }

            /// <summary>
            /// Returns a human-readable relative time string compared to a specific date
            /// </summary>
            /// <param name="referenceDate">The date to compare against</param>
            /// <returns>A relative time string</returns>
            public string ToRelativeTime(DateTime referenceDate)
            {
                var timeSpan = referenceDate - date;
                var isFuture = timeSpan.TotalSeconds < 0;
                var absoluteSpan = timeSpan.Duration();

                string result;

                if (absoluteSpan.TotalSeconds < 60)
                {
                    result = "just now";
                }
                else if (absoluteSpan.TotalMinutes < 60)
                {
                    var minutes = (int)absoluteSpan.TotalMinutes;
                    result = $"{minutes} minute{(minutes == 1 ? "" : "s")}";
                }
                else if (absoluteSpan.TotalHours < 24)
                {
                    var hours = (int)absoluteSpan.TotalHours;
                    result = $"{hours} hour{(hours == 1 ? "" : "s")}";
                }
                else if (absoluteSpan.TotalDays < 30)
                {
                    var days = (int)absoluteSpan.TotalDays;
                    result = $"{days} day{(days == 1 ? "" : "s")}";
                }
                else if (absoluteSpan.TotalDays < 365)
                {
                    var months = (int)(absoluteSpan.TotalDays / 30);
                    result = $"{months} month{(months == 1 ? "" : "s")}";
                }
                else
                {
                    var years = (int)(absoluteSpan.TotalDays / 365);
                    result = $"{years} year{(years == 1 ? "" : "s")}";
                }

                if (result == "just now")
                {
                    return result;
                }

                return isFuture ? $"in {result}" : $"{result} ago";
            }

            /// <summary>
            /// Determines whether the date is today
            /// </summary>
            /// <returns>True if the date is today, otherwise false</returns>
            /// <example>
            /// <code>
            /// DateTime.Today.IsToday() // Returns true
            /// DateTime.Today.AddDays(-1).IsToday() // Returns false
            /// </code>
            /// </example>
            public bool IsToday()
            {
                return date.Date == DateTime.Today;
            }

            /// <summary>
            /// Determines whether the date is tomorrow
            /// </summary>
            /// <returns>True if the date is tomorrow, otherwise false</returns>
            public bool IsTomorrow()
            {
                return date.Date == DateTime.Today.AddDays(1);
            }

            /// <summary>
            /// Determines whether the date is yesterday
            /// </summary>
            /// <returns>True if the date is yesterday, otherwise false</returns>
            public bool IsYesterday()
            {
                return date.Date == DateTime.Today.AddDays(-1);
            }

            /// <summary>
            /// Returns a new DateTime set to the start of the week
            /// </summary>
            /// <param name="startOfWeek">The day considered as the start of the week (defaults to Sunday)</param>
            /// <returns>A DateTime set to the start of the week</returns>
            /// <example>
            /// <code>
            /// var date = new DateTime(2024, 1, 10); // Wednesday
            /// date.StartOfWeek(DayOfWeek.Monday) // Returns Monday of that week
            /// </code>
            /// </example>
            public DateTime StartOfWeek(DayOfWeek startOfWeek = DayOfWeek.Sunday)
            {
                var diff = (7 + (date.DayOfWeek - startOfWeek)) % 7;
                return date.AddDays(-diff).Date;
            }

            /// <summary>
            /// Returns a new DateTime set to the end of the week
            /// </summary>
            /// <param name="startOfWeek">The day considered as the start of the week (defaults to Sunday)</param>
            /// <returns>A DateTime set to the end of the week</returns>
            /// <example>
            /// <code>
            /// var date = new DateTime(2024, 1, 10); // Wednesday
            /// date.EndOfWeek(DayOfWeek.Monday) // Returns Sunday of that week at 23:59:59.999
            /// </code>
            /// </example>
            public DateTime EndOfWeek(DayOfWeek startOfWeek = DayOfWeek.Sunday)
            {
                return date.StartOfWeek(startOfWeek).AddDays(7).AddTicks(-1);
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
