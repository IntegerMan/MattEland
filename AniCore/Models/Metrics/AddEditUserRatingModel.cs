using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Ani.Core.Models.Users;

namespace Ani.Core.Models.Metrics
{
    /// <summary>
    /// A model used for adding or editing a user rating
    /// </summary>
    public class AddEditUserRatingModel : RatingModelBase
	{

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