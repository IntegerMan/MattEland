using System.Web.Mvc;
using Ani.Core.Models.Metrics;
using Ani.Core.Services;

namespace AniWebApp.Controllers
{
	/// <summary>
	/// An MVC Controller governing ratings-related activities
	/// </summary>
	[Authorize]
	public class RatingsController : CustomController
	{
        private readonly RatingsService _ratingsService;

        public RatingsController() : this(null)
		{
		}

		public RatingsController(ApplicationRoleManager roleManager) : base(roleManager)
		{
		    _ratingsService = new RatingsService(this.Entities);
		}

		/// <summary>
		/// Goes to the main index page for the ratings application.
		/// </summary>
		/// <returns>The view for the main ratings application.</returns>
		[HttpGet]
		[Route("Ratings")]
		[Authorize]
		public ActionResult Index()
		{
			var model = new RatingsModel
			{
				Ratings = _ratingsService.GetLatestRatingInfoForUser(this.GetUserId())
			};

			return View(model);
		}

	    [HttpGet]
		[Route(@"Ratings/{ratingId}/Add")]
		[Authorize]
		public ActionResult AddEntry(int ratingId)
		{
			var rating = _ratingsService.GetRating(ratingId);
			if (rating == null)
			{
				return RedirectToAction("NotFound", "Error");
			}

	        var userEntity = this.GetUserEntity();

	        var model = RatingsService.BuildNewRatingEntryModel(rating, userEntity);

			return View(model);
		}

	    [HttpPost]
		[Route(@"Ratings/{ratingId}/Add")]
		[Authorize]
		[ValidateAntiForgeryToken]
		public ActionResult AddEntryPost(int ratingId, AddEditRatingModel model)
		{
			if (ModelState.IsValid)
			{
				var userId = this.GetUserId();

			    var rating = _ratingsService.GetRating(ratingId);
				if (rating == null || model == null || userId <= 0)
				{
					return RedirectToAction("NotFound", "Error");
				}

			    _ratingsService.AddUserRating(rating, model, userId);

			    return RedirectToAction("Index");
			}

			return View(model);
		}


	    [HttpGet]
		[Route(@"Ratings/{ratingId}")]
		[Authorize]
		public ActionResult History(int ratingId)
		{
			var rating = _ratingsService.GetRating(ratingId);
			if (rating == null)
			{
				return RedirectToAction("NotFound", "Error");
			}

			return View();
		}
	}
}