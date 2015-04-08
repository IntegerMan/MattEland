using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ani.Core.Models.Metrics
{
    public class UserRatingHistoryEntry
    {
        public int UserId{ get; set; }

        [DisplayName("Rating")]
        public int RatingValue { get; set; }

        [DisplayName("Entry Date")]
        [DataType(DataType.Date)]
        public DateTime EntryDate { get; set; }

        [DisplayName("Created Time")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedTimeUTC { get; set; }

        [DisplayName("Modified Time")]
        [DataType(DataType.DateTime)]
        public DateTime ModifiedTimeUTC{ get; set; }

        [DataType(DataType.MultilineText)]
        public string Comments{ get; set; }

        public int Id{ get; set; }

		/// <summary>
		/// Gets or sets the rating.
		/// </summary>
		/// <value>The rating.</value>
		public RatingModel Rating { get; set; }
    }
}