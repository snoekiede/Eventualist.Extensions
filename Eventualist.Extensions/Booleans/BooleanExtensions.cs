using System;

namespace Eventualist.Extensions.Booleans
{
    /// <summary>
    /// Extension methods for boolean values
    /// </summary>
    public static class BooleanExtensions
    {
        extension(bool variable)
        {

            /// <summary>
            /// Prefixes the keyword with a negation when the value is false
            /// </summary>
            /// <param name="keyword">The keyword to be negated</param>
            /// <param name="negation">The negation to be used</param>
            /// <returns>The keyword with optional negation</returns>
            public string AddNot(string keyword, string negation = "not")
            {
                ArgumentNullException.ThrowIfNull(keyword);
                return variable ? keyword : $"{negation} {keyword}";
            }

            /// <summary>
            /// Returns a confirmation or negation string depending on the value
            /// </summary>
            /// <param name="yesString">The confirmation</param>
            /// <param name="noString">The negation</param>
            /// <returns>Either the confirmation or negation string</returns>
            public string ToYesOrNo(string yesString = "yes", string noString = "no")
            {
                ArgumentNullException.ThrowIfNull(yesString);
                ArgumentNullException.ThrowIfNull(noString);
                return variable ? yesString : noString;
            }
        }

        extension(bool? variable)
        {
            

            /// <summary>
            /// Prefixes the keyword with a negation when the value is false, or with the null text when it is null
            /// </summary>
            /// <param name="keyword">The keyword to be negated</param>
            /// <param name="negation">The negation to be used</param>
            /// <param name="nullText">Text to use when variable is null</param>
            /// <returns>The keyword with optional negation or null text</returns>
            public string AddNot(string keyword, string negation = "not",
                string nullText = "unknown if")
            {
                ArgumentNullException.ThrowIfNull(keyword);
                return variable.HasValue
                    ? variable.Value ? keyword : $"{negation} {keyword}"
                    : $"{nullText} {keyword}";
            }



            /// <summary>
            /// Returns a confirmation, negation, or null-value string depending on the nullable value
            /// </summary>
            /// <param name="yesString">The confirmation</param>
            /// <param name="noString">The negation</param>
            /// <param name="nullString">The value when variable is null</param>
            /// <returns>Either the confirmation, negation, or null string</returns>
            public string ToYesOrNo(string yesString = "yes", string noString = "no",
                string nullString = "unknown")
            {
                ArgumentNullException.ThrowIfNull(yesString);
                ArgumentNullException.ThrowIfNull(noString);
                ArgumentNullException.ThrowIfNull(nullString);
                return variable.HasValue ? (variable.Value ? yesString : noString) : nullString;
            }

            /// <summary>
            /// Converts the nullable boolean to a lowercase or uppercase string representation
            /// </summary>
            /// <param name="uppercase">Whether to use uppercase</param>
            /// <returns>The lowercase or uppercase string representation; an empty string when the value is null</returns>
            public string ToString(bool uppercase = false)
            {
                string result = variable.ToString().ToLowerInvariant();
                return uppercase ? result.ToUpperInvariant() : result;
            }
        }
    }
}
