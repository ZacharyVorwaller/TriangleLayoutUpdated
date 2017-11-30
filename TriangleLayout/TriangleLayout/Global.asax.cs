using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace TriangleLayout
{
    public class Global : HttpApplication
    {
        /// <summary>
        /// Handles the Start event of the Application control.  Configures the log4net xml configurator.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            XmlConfigurator.Configure(new System.IO.FileInfo("log4net.xml"));
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}