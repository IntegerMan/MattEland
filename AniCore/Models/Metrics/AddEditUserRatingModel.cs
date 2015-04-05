using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Ani.Core.Models.Users;

namespace Ani.Core.Models.Metrics
{
    /// <summary>
    /// A model used for adding or editing a user rating
    /// </summary>
    public class AddEditUserRatingModel
	{
        /// <summary>
        /// Gets or sets the rating value.
        /// </summary>
        /// <value>The rating value.</value>
        [Required]
		[DisplayName("Rating")]
		public int RatingValue { get; set; }

        /// <summary>
        /// Gets or sets the rating model.
        /// </summary>
        /// <value>The rating model.</value>
        public RatingModel Rating { get; set; }

        /// <summary>
        /// Gets or sets the entry date.
        /// </summary>
        /// <value>The entry date.</value>
        [Required]
		[DisplayName("Date")]
		[DataType(DataType.Date)]
		public System.DateTime EntryDate { get; set; }

        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        /// <value>The comments.</value>
        [DisplayName("Comments")]
		[DataType(DataType.MultilineText)]

		public string Comments { get; set; }

        /// <summary>
        /// Gets or sets the created time in UTC.
        /// </summary>
        /// <value>The created time in UTC.</value>
        [Required]
		[DisplayName("Created Date")]
		[DataType(DataType.DateTime)]
		public System.DateTime CreatedTimeUTC { get; set; }

        /// <summary>
        /// Gets or sets the modified time in UTC format.
        /// </summary>
        /// <value>The modified time in UTC.</value>
        [Required]
		[DisplayName("Modified Date")]
		[DataType(DataType.DateTime)]
		public System.DateTime ModifiedTimeUTC { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>The user.</value>
        public UserModel User { get; set; }
	}
}