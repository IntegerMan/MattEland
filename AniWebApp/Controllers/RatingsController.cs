using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AniWebApp.Models.Metrics;

namespace AniWebApp.Controllers
{
	/// <summary>
	/// An MVC Controller governing ratings-related activities
	/// </summary>
	[Authorize]
	public class RatingsController : CustomController
	{

		public RatingsController() : this(null)
		{
		}

		public RatingsController(ApplicationRoleManager roleManager) : base(roleManager)
		{
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
				Ratings = this.Entities.RatingsWithLatestInfoForUserSelect(this.GetUserId()).ToList()
			};

			return View(model);
		}

		[HttpGet]
		[Route(@"Ratings/{RatingId}/Add")]
		[Authorize]
		public ActionResult AddEntry(int RatingId)
		{
			var rating = this.Entities.Ratings.FirstOrDefault(r => r.Id == RatingId);
			if (rating == null)
			{
				return RedirectToAction("NotFound", "Error");
			}

			var model = new AddEditRatingModel
			{
				Rating = rating,
				RatingValue = rating.MinValue,
				EntryDate = DateTime.Today,
				CreatedTimeUTC = DateTime.Now.ToUniversalTime(),
				ModifiedTimeUTC = DateTime.Now.ToUniversalTime(),
				User = this.GetUserEntity()
			};

			return View(model);
		}

		[HttpPost]
		[Route(@"Ratings/{RatingId}/Add")]
		[Authorize]
		[ValidateAntiForgeryToken]
		public ActionResult AddEntryPost(int RatingId, AddEditRatingModel model)
		{
			if (ModelState.IsValid)
			{
				var userId = this.GetUserId();

				var rating = this.Entities.Ratings.FirstOrDefault(r => r.Id == RatingId);
				if (rating == null || model == null || userId <= 0)
				{
					return RedirectToAction("NotFound", "Error");
				}

				var entryDateUtc = new DateTime(model.EntryDate.Year, model.EntryDate.Month, model.EntryDate.Day, 0, 0, 0, DateTimeKind.Utc);
				this.Entities.InsertUpdateUserRating(userId, RatingId, model.Comments, model.RatingValue, entryDateUtc);

				return RedirectToAction("Index");
			}

			return View(model);
		}


		[HttpGet]
		[Route(@"Ratings/{RatingId}")]
		[Authorize]
		public ActionResult History(int RatingId)
		{
			var rating = this.Entities.Ratings.FirstOrDefault(r => r.Id == RatingId);
			if (rating == null)
			{
				return RedirectToAction("NotFound", "Error");
			}

			return View();
		}
	}
}