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
        /// Gets the table row styling class given the severity.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="severityId">The severity identifier.</param>
        /// <returns>The table row class.</returns>
        public static string GetTableRowClass(this HtmlHelper helper, int severityId)
        {
            if (severityId >= 3)
            {
                return "danger";
            }

            return severityId >= 2 ? "warning" : "info";
        }

        /// <summary>
        /// Gets the panel class given the severity.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="severityId">The severity identifier.</param>
        /// <param name="defaultClass">The default class to use if no severity. Defaults to panel-info.</param>
        /// <returns>The panel class.</returns>
        public static string GetPanelClass(this HtmlHelper helper, int severityId, string defaultClass = "panel-info")
        {
            if (severityId >= 3)
            {
                return "panel-danger";
            }

            return severityId >= 2 ? "panel-warning" : defaultClass;
        }

        /// <summary>
        /// Gets the label class given the severity.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="severityId">The severity identifier.</param>
        /// <param name="defaultClass">The default class to use if no severity. Defaults to label-info.</param>
        /// <returns>The panel class.</returns>
        public static string GetLabelClass(this HtmlHelper helper, int severityId, string defaultClass = "label-info")
        {
            if (severityId >= 3)
            {
                return "label-danger";
            }

            return severityId >= 2 ? "label-warning" : defaultClass;
        }

        /// <summary>
        /// Gets the text class given the severity.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="severityId">The severity identifier.</param>
        /// <returns>The text class.</returns>
        public static string GetTextClass(this HtmlHelper helper, int severityId)
        {
            if (severityId >= 3)
            {
                return "text-danger";
            }

            return severityId >= 2 ? "text-warning" : string.Empty;
        }
        public static string GetThemeCss(this HtmlHelper helper)
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