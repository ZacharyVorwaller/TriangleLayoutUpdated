using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using TriangleLayout.Classes.Helpers;

namespace TriangleLayout.Classes
{
    /// <summary>
    /// Represents a Triangle object
    /// </summary>
    public class Triangle
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Triangle));
        private string currentIdentifier = string.Empty;

        /// <summary>
        /// Gets the current identifier for the Triangle instance.
        /// </summary>
        /// <value>
        /// The current triangle identifier (ie. 'D10').
        /// </value>
        public string CurrentIdentifier
        {
            get { return currentIdentifier; }
        }

        /// <summary>
        /// Finds the triangle given a set of three coordinates.
        /// </summary>
        /// <param name="xcords">List of int containing the x coordinates.</param>
        /// <param name="ycords">List of int containing the y coordinates.</param>
        /// <param name="nonHypotenuse">The non hypotenuse length for a single triangle.</param>
        /// <returns>Bool identifing if the find was successfull.</returns>
        public bool FindTriangle(List<int> xcords, List<int> ycords, int nonHypotenuse)
        {
            try
            {
                xcords.Sort();
                ycords.Sort();

                var xMedianValue = xcords[1];
                var yMedianValue = ycords[1];
                var xMaxValue = xcords.Max();
                var yMaxValue = ycords.Max();

                string row = ConvertHelper.IntToLetters(yMaxValue / nonHypotenuse);
                string column = ((xMedianValue / nonHypotenuse) + (xMaxValue / nonHypotenuse)).ToString();

                currentIdentifier = row + column;
            }
            catch(Exception ex)
            {
                log.Error("Error with FindTriangle", ex);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Draws the triangle layout with the identified triangle highlighted from FindTriangle and returns the HTML to display the result.
        /// </summary>
        /// <param name="nonHypotenuse">The non hypotenuse length for a single triangle.</param>
        /// <param name="row">The max row value in Char (A-Z)</param>
        /// <param name="column">The max column value in int (12-22).</param>
        /// <returns>Returns a string containg either the HTML result to display or an error string.</returns>
        public string DrawTriangle(string nonHypotenuse, Char row, int column)
        {
            try
            {
                if (currentIdentifier == string.Empty)
                {
                    return "You must first find a triangle";
                }
                StringBuilder html = new StringBuilder();
                bool isLeft = true;

                for (char c = 'A'; c <= row; c++)
                {
                    for (int i = 1; i <= column; i++)
                    {
                        bool highlight = ((c.ToString() + i.ToString()) == currentIdentifier);
                        if (isLeft)
                        {
                            html.Append("<div style=\"width: " + nonHypotenuse + "px; padding-bottom: " + nonHypotenuse + "px;\" class=\"triangle-left" + (highlight ? " highlighted-left" : "") + "\"></div>");
                        }
                        else
                        {
                            html.Append("<div style=\"padding-left:" + nonHypotenuse + "px; padding-top: " + nonHypotenuse + "px; margin-left:-" + nonHypotenuse + "px;\" class=\"triangle-right" + (highlight ? " highlighted-right" : "") + "\"></div>");
                        }
                        isLeft = !isLeft;
                    }
                    html.Append("<div style=\"float:left;clear:both;\"></div>");
                }
                return html.ToString();
            }
            catch (Exception ex)
            {
                log.Error("Error with DrawTriangle", ex);
                return "There was an issue generating the display, please verify values";
            }
        }
    }
}