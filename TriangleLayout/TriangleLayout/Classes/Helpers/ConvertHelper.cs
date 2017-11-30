using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TriangleLayout.Classes.Helpers
{
    /// <summary>
    /// Helper class used for converting values
    /// </summary>
    public class ConvertHelper
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ConvertHelper));
        /// <summary>
        /// Converts an int to a char letter.
        /// </summary>
        /// <param name="value">The int needing to be converted.</param>
        /// <returns>A Char with the letter equivalent of the int passed in.</returns>
        public static string IntToLetters(int value)
        {
            try
            {
                string result = string.Empty;
                while (--value >= 0)
                {
                    result = (char)('A' + value % 26) + result;
                    value /= 26;
                }
                return result;
            }
            catch(Exception ex)
            {
                log.Error("Error with IntToLetters", ex);
                return "A";
            }
        }
    }
}