using System.Web.Mvc;

using Ani.Core.Models;

using MattEland.Common;
using MattEland.Common.Annotations;

namespace AniWebApp.Controllers
{
    /// <summary>
    ///     An MVC Controller controlling the application root.
    /// </summary>
    [Route(@"Search")]
    public sealed class SearchController : CustomController
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SearchController"/> class.
        /// </summary>
        public SearchController() : this(null)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="CustomController" /> class.
        /// </summary>
        /// <param name="roleManager"> The role manager. </param>
        public SearchController([CanBeNull] ApplicationRoleManager roleManager) : base(roleManager)
        {
        }

        /// <summary>
        ///     Serves up the search page's view.
        /// </summary>
        /// <returns>
        ///     The search page view.
        /// </returns>
        [HttpGet]
        [AllowAnonymous]
        [Route(@"Search")]
        public ActionResult Search([CanBeNull] SearchModel search)
        {
            // Ensure search exists
            search = search ?? new SearchModel();

            // If there was a query, run it
            if (search.Query.HasText())
            {
                ShowSuccess("0 Results Found");
            }

            // Serve up the view
            return View(search);
        }

    }
}
