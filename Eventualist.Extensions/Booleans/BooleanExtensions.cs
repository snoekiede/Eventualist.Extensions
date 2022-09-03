using System;
using System.Collections.Generic;
using System.Text;

namespace Eventualist.Extensions.Booleans
{
    public static class BooleanExtensions
    {
        /// <summary>
        /// Add a negation to the keyword, if the variable is false
        /// </summary>
        /// <param name="variable">the underlying variable</param>
        /// <param name="keyword">the keyword to be negated</param>
        /// <param name="negation">the negation to be used</param>
        /// <returns></returns>
        public static string AddNot(this bool variable, string keyword, string negation = "not")
        {
            return variable ? keyword : $"{negation} {keyword}";
        }

        /// <summary>
        /// Transforms variable to a confirmation or a negation depending to its value
        /// </summary>
        /// <param name="variable">the underlying variable</param>
        /// <param name="yesString">the confirmation</param>
        /// <param name="noString">the negation</param>
        /// <returns></returns>
        public static string ToYesOrNo(this bool variable, string yesString = "yes", string noString = "no")
        {
            return variable ? yesString : noString;
        }
    }
}
