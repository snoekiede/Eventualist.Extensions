using System;
using System.Globalization;

namespace Eventualist.Extensions.Numerics
{
    /// <summary>
    /// Extension methods for numeric types
    /// </summary>
    public static class NumericExtensions
    {
        extension(int number)
        {
            /// <summary>
            /// Determines whether the number is even
            /// </summary>
            /// <returns>True if the number is even, otherwise false</returns>
            /// <example>
            /// <code>
            /// 4.IsEven() // Returns true
            /// 5.IsEven() // Returns false
            /// </code>
            /// </example>
            public bool IsEven()
            {
                return number % 2 == 0;
            }

            /// <summary>
            /// Determines whether the number is odd
            /// </summary>
            /// <returns>True if the number is odd, otherwise false</returns>
            /// <example>
            /// <code>
            /// 5.IsOdd() // Returns true
            /// 4.IsOdd() // Returns false
            /// </code>
            /// </example>
            public bool IsOdd()
            {
                return number % 2 != 0;
            }

            /// <summary>
            /// Determines whether the number is between two values (inclusive)
            /// </summary>
            /// <param name="min">The minimum value</param>
            /// <param name="max">The maximum value</param>
            /// <returns>True if the number is between min and max (inclusive), otherwise false</returns>
            /// <example>
            /// <code>
            /// 5.IsBetween(1, 10) // Returns true
            /// 15.IsBetween(1, 10) // Returns false
            /// </code>
            /// </example>
            public bool IsBetween(int min, int max)
            {
                return number >= min && number <= max;
            }

            /// <summary>
            /// Clamps the number to be within the specified range
            /// </summary>
            /// <param name="min">The minimum value</param>
            /// <param name="max">The maximum value</param>
            /// <returns>The clamped value</returns>
            /// <example>
            /// <code>
            /// 15.Clamp(0, 10) // Returns 10
            /// (-5).Clamp(0, 10) // Returns 0
            /// 5.Clamp(0, 10) // Returns 5
            /// </code>
            /// </example>
            public int Clamp(int min, int max)
            {
                if (min > max)
                {
                    throw new ArgumentException("Min value cannot be greater than max value", nameof(min));
                }

                return Math.Clamp(number, min, max);
            }

            /// <summary>
            /// Converts a number to its ordinal string representation
            /// </summary>
            /// <returns>The ordinal string (e.g., "1st", "2nd", "3rd", "4th")</returns>
            /// <example>
            /// <code>
            /// 1.ToOrdinal() // Returns "1st"
            /// 2.ToOrdinal() // Returns "2nd"
            /// 3.ToOrdinal() // Returns "3rd"
            /// 21.ToOrdinal() // Returns "21st"
            /// 112.ToOrdinal() // Returns "112th"
            /// </code>
            /// </example>
            public string ToOrdinal()
            {
                if (number <= 0)
                {
                    return number.ToString(CultureInfo.InvariantCulture);
                }

                // Special cases for 11, 12, 13
                int lastTwoDigits = number % 100;
                if (lastTwoDigits >= 11 && lastTwoDigits <= 13)
                {
                    return $"{number}th";
                }

                // Check last digit
                int lastDigit = number % 10;
                return lastDigit switch
                {
                    1 => $"{number}st",
                    2 => $"{number}nd",
                    3 => $"{number}rd",
                    _ => $"{number}th"
                };
            }
        }

        extension(long number)
        {
            /// <summary>
            /// Determines whether the number is even
            /// </summary>
            /// <returns>True if the number is even, otherwise false</returns>
            public bool IsEven()
            {
                return number % 2 == 0;
            }

            /// <summary>
            /// Determines whether the number is odd
            /// </summary>
            /// <returns>True if the number is odd, otherwise false</returns>
            public bool IsOdd()
            {
                return number % 2 != 0;
            }

            /// <summary>
            /// Determines whether the number is between two values (inclusive)
            /// </summary>
            /// <param name="min">The minimum value</param>
            /// <param name="max">The maximum value</param>
            /// <returns>True if the number is between min and max (inclusive), otherwise false</returns>
            public bool IsBetween(long min, long max)
            {
                return number >= min && number <= max;
            }

            /// <summary>
            /// Clamps the number to be within the specified range
            /// </summary>
            /// <param name="min">The minimum value</param>
            /// <param name="max">The maximum value</param>
            /// <returns>The clamped value</returns>
            public long Clamp(long min, long max)
            {
                if (min > max)
                {
                    throw new ArgumentException("Min value cannot be greater than max value", nameof(min));
                }

                return Math.Clamp(number, min, max);
            }
        }

        extension(double number)
        {
            /// <summary>
            /// Determines whether the number is between two values (inclusive)
            /// </summary>
            /// <param name="min">The minimum value</param>
            /// <param name="max">The maximum value</param>
            /// <returns>True if the number is between min and max (inclusive), otherwise false</returns>
            public bool IsBetween(double min, double max)
            {
                return number >= min && number <= max;
            }

            /// <summary>
            /// Clamps the number to be within the specified range
            /// </summary>
            /// <param name="min">The minimum value</param>
            /// <param name="max">The maximum value</param>
            /// <returns>The clamped value</returns>
            public double Clamp(double min, double max)
            {
                if (min > max)
                {
                    throw new ArgumentException("Min value cannot be greater than max value", nameof(min));
                }

                return Math.Clamp(number, min, max);
            }
        }

        extension(decimal number)
        {
            /// <summary>
            /// Determines whether the number is between two values (inclusive)
            /// </summary>
            /// <param name="min">The minimum value</param>
            /// <param name="max">The maximum value</param>
            /// <returns>True if the number is between min and max (inclusive), otherwise false</returns>
            public bool IsBetween(decimal min, decimal max)
            {
                return number >= min && number <= max;
            }

            /// <summary>
            /// Clamps the number to be within the specified range
            /// </summary>
            /// <param name="min">The minimum value</param>
            /// <param name="max">The maximum value</param>
            /// <returns>The clamped value</returns>
            public decimal Clamp(decimal min, decimal max)
            {
                if (min > max)
                {
                    throw new ArgumentException("Min value cannot be greater than max value", nameof(min));
                }

                return Math.Clamp(number, min, max);
            }
        }
    }
}
