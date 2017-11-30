using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TriangleLayout.Classes;
using TriangleLayout.Classes.Helpers;

namespace TriangleLayout
{
    public partial class _Default : Page
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(_Default));

        protected void Page_Load(object sender, EventArgs e)
        {
            

        }

        /// <summary>
        /// Handles the Click event of the ButtonSolve control.  Collects the x and y coordinates from the TextBox controls and performs both FindTriangle and DrawTriangle to then display the result on the web page.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void ButtonSolve_Click(object sender, EventArgs e)
        {
            string result = string.Empty;
            try
            {
                Triangle currentTriangle = new Triangle();
                int x1 = -1, x2 = -1, x3 = -1, y1 = -1, y2 = -1, y3 = -1;
                List<int> xcords = new List<int>();
                List<int> ycords = new List<int>();
                if (Int32.TryParse(TextBoxNonHypotenuseLength.Text, out int nonHypotenuse) && nonHypotenuse <= 75 && nonHypotenuse >= 10)
                {
                    int column = 12;
                    Char row = 'F';
                    if (!Char.TryParse(TextBoxMaxRow.Text, out row))
                    {
                        result += "Invalid row, using default of 'F'<br/>";
                    }
                    if (!Int32.TryParse(TextBoxMaxColumn.Text, out column) || column % 2 != 0 || column < 12 || column > 42)
                    {
                        column = 12;
                        result += "Invalid column, using default of 12<br/>";
                    }

                    if (Int32.TryParse(TextBoxV1x.Text, out x1) && Int32.TryParse(TextBoxV2x.Text, out x2) && Int32.TryParse(TextBoxV3x.Text, out x3)
                        && Int32.TryParse(TextBoxV1y.Text, out y1) && Int32.TryParse(TextBoxV2y.Text, out y2) && Int32.TryParse(TextBoxV3y.Text, out y3))
                    {
                        xcords.Add(x1);
                        xcords.Add(x2);
                        xcords.Add(x3);
                        ycords.Add(y1);
                        ycords.Add(y2);
                        ycords.Add(y3);

                        if (((xcords.Max() - xcords.Min()) == 10) && ((ycords.Max() - ycords.Min()) == 10))
                        {
                            if (currentTriangle.FindTriangle(xcords, ycords, 10)) //Hard coded to 10 so the coordinates match the requirements specified
                            {
                                result += "Triangle Selected: " + currentTriangle.CurrentIdentifier;
                                result += "<br/><br/>";
                                result += currentTriangle.DrawTriangle(nonHypotenuse.ToString(), row, column);
                            }
                            else
                            {
                                log.Debug("Issue finding triangle based on coordinates");
                                result += "Issue finding triangle based on coordinates";
                            }
                        }
                        else
                        {
                            log.Debug("Coordinates for multiple triangles entered");
                            result += "Please enter coordinates for just one triangle";
                        }
                    }
                    else
                    {
                        log.Debug("Invaild number entered");
                        result += "Please enter a vaild number for each coordinate";
                    }
                }
                else
                {
                    log.Debug("Value not between 10 and 50");
                    result += "Please enter a px value between 10 and 50";
                }
            }
            catch(Exception ex)
            {
                result += "Unable to solve, please check values.";
                log.Error("Error with ButtonSolve_Click", ex);
            }
            Results.InnerHtml = result;
        }
    }
}