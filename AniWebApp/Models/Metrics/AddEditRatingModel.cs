using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AniWebApp.Models.Metrics
{
	public class AddEditRatingModel
	{
		[Required]
		[DisplayName("Rating")]
		public int RatingValue { get; set; }

		public Rating Rating { get; set; }

		[Required]
		[DisplayName("Date")]
		[DataType(DataType.Date)]
		public System.DateTime EntryDate { get; set; }

		[DisplayName("Comments")]
		[DataType(DataType.MultilineText)]

		public string Comments { get; set; }

		[Required]
		[DisplayName("Created Date")]
		[DataType(DataType.DateTime)]
		public System.DateTime CreatedTimeUTC { get; set; }

		[Required]
		[DisplayName("Modified Date")]
		[DataType(DataType.DateTime)]
		public System.DateTime ModifiedTimeUTC { get; set; }

		public User User { get; set; }
	}
}