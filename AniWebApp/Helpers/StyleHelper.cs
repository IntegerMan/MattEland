using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;

namespace AniWebApp.Helpers
{
    /// <summary>
    /// MVC extensions for styling elements
    /// </summary>
    public static class StyleHelper
    {

        /// <summary>
        /// Gets the panel class given the severity.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="severityId">The severity identifier.</param>
        /// <returns>The panel class.</returns>
        public static string GetPanelClass(this HtmlHelper helper, int severityId)
        {
            if (severityId >= 3)
            {
                return "panel-danger";
            }

            return severityId >= 2 ? "panel-warning" : "panel-info";
        }

        public static IHtmlString GetThemeCSS(this HtmlHelper helper)
        {
            return Styles.Render("~/content/bootstrap.css");
        }

        public static IHtmlString GetThemeJavaScript(this HtmlHelper helper)
        {
            return Scripts.Render("~/scripts/bootstrap-386.js");
        }
    }
}