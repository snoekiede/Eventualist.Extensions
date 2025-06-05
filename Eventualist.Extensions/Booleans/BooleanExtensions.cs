using System;

namespace Eventualist.Extensions.Booleans
{
    /// <summary>
    /// Extension methods for boolean values
    /// </summary>
    public static class BooleanExtensions
    {
        /// <summary>
        /// Add a negation to the keyword, if the variable is false
        /// </summary>
        /// <param name="variable">The underlying variable</param>
        /// <param name="keyword">The keyword to be negated</param>
        /// <param name="negation">The negation to be used</param>
        /// <returns>The keyword with optional negation</returns>
        public static string AddNot(this bool variable, string keyword, string negation = "not")
        {
            ArgumentNullException.ThrowIfNull(keyword);
            return variable ? keyword : $"{negation} {keyword}";
        }

        /// <summary>
        /// Add a negation to the keyword, if the nullable variable is false or null
        /// </summary>
        /// <param name="variable">The nullable boolean variable</param>
        /// <param name="keyword">The keyword to be negated</param>
        /// <param name="negation">The negation to be used</param>
        /// <param name="nullText">Text to use when variable is null</param>
        /// <returns>The keyword with optional negation or null text</returns>
        public static string AddNot(this bool? variable, string keyword, string negation = "not",
            string nullText = "unknown if")
        {
            ArgumentNullException.ThrowIfNull(keyword);
            return variable.HasValue
                ? variable.Value ? keyword : $"{negation} {keyword}"
                : $"{nullText} {keyword}";
        }

        /// <summary>
        /// Transforms variable to a confirmation or a negation depending on its value
        /// </summary>
        /// <param name="variable">The underlying variable</param>
        /// <param name="yesString">The confirmation</param>
        /// <param name="noString">The negation</param>
        /// <returns>Either the confirmation or negation string</returns>
        public static string ToYesOrNo(this bool variable, string yesString = "yes", string noString = "no")
        {
            ArgumentNullException.ThrowIfNull(yesString);
            ArgumentNullException.ThrowIfNull(noString);
            return variable ? yesString : noString;
        }

        /// <summary>
        /// Transforms nullable variable to a confirmation, negation, or unknown value
        /// </summary>
        /// <param name="variable">The nullable boolean variable</param>
        /// <param name="yesString">The confirmation</param>
        /// <param name="noString">The negation</param>
        /// <param name="nullString">The value when variable is null</param>
        /// <returns>Either the confirmation, negation, or null string</returns>
        public static string ToYesOrNo(this bool? variable, string yesString = "yes", string noString = "no",
            string nullString = "unknown")
        {
            ArgumentNullException.ThrowIfNull(yesString);
            ArgumentNullException.ThrowIfNull(noString);
            ArgumentNullException.ThrowIfNull(nullString);
            return variable.HasValue ? (variable.Value ? yesString : noString) : nullString;
        }

        /// <summary>
        /// Converts a boolean to its string representation with a specified case
        /// </summary>
        /// <param name="variable">The boolean value to convert</param>
        /// <param name="uppercase">Whether to use uppercase</param>
        /// <returns>String representation of the boolean</returns>
        public static string ToString(this bool variable, bool uppercase = false)
        {
            string result = variable.ToString().ToLowerInvariant();
            return uppercase ? result.ToUpperInvariant() : result;
        }
    }
}
