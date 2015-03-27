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
        private const string DefaultCSS = "~/content/bootstrap.css";
        private const string DefaultJavaScript = "~/scripts/bootstrap-386.js";

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

        public static string GetThemeCSS(this HtmlHelper helper)
        {
            var entities = new AniEntities();
            var user = UserHelper.GetCurrentUserEntity(entities, HttpContext.Current.User);

            return user != null ? user.WebTheme.WebCssURL : DefaultCSS;
        }

        public static string GetThemeJavaScript(this HtmlHelper helper)
        {
            var entities = new AniEntities();
            var user = UserHelper.GetCurrentUserEntity(entities, HttpContext.Current.User);

            return user == null ? DefaultJavaScript : user.WebTheme.WebJsUrl;
        }
    }
}