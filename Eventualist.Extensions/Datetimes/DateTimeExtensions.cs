using System;
using System.Collections.Generic;
using System.Text;

namespace Eventualist.Extensions.Datetimes
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// format date for the datepicker (this is specific to my website and might be removed)
        /// </summary>
        /// <param name="date">the underlying date</param>
        /// <returns>a formatted string</returns>
        public static string FormatDateForPicker(this DateTime date)
        {
            return date.ToString("yyyy/MM/dd");
        }

        /// <summary>
        /// format time for the datepicker (this is specific to my website and might be removed)
        /// </summary>
        /// <param name="date">the underlying date</param>
        /// <returns>a formatted string</returns>
        public static string FormatTimeForPicker(this DateTime date)
        {
            return date.ToString("HH:mm");
        }


        public static string FormatDateTimeForPicker(this DateTime date)
        {
            return date.ToString("yyyy/MM/dd HH:mm");
        }
    }
}
