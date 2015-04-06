using System;

namespace Ani.Core.Models.Metrics
{
    public class UserRatingHistoryEntry
    {
        public int UserId{ get; set; }

        public int RatingValue { get; set; }

        public DateTime EntryDate { get; set; }

        public DateTime CreatedTimeUTC { get; set; }

        public DateTime ModifiedTimeUTC{ get; set; }

        public string Comments{ get; set; }

        public int Id{ get; set; }

		/// <summary>
		/// Gets or sets the rating.
		/// </summary>
		/// <value>The rating.</value>
		public RatingModel Rating { get; set; }
    }
}