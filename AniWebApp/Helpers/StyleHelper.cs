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
        private const int DefaultThemeId = 1;

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
            var theme = GetTheme();
            return theme.WebCssURL;
        }

        private static WebTheme GetTheme()
        {
            var entities = new AniEntities();

            var user = UserHelper.GetCurrentUserEntity(entities, HttpContext.Current.User);
            var theme = user != null ? user.WebTheme : entities.WebThemes.First(t => t.ID == DefaultThemeId);
            return theme;
        }

        public static string GetThemeJavaScript(this HtmlHelper helper)
        {
            var theme = GetTheme();
            return theme.WebJsUrl;
        }
    }
}