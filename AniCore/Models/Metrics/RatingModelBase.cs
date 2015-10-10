using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ani.Core.Models.Metrics
{
	public class RatingModelBase //: IValidatableObject
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
		/// Validates the model.
		/// </summary>
		/// <param name="validationContext">The validation context.</param>
		/// <returns>A yielded collection of validation results.</returns>
		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (this.RatingValue < this.Rating.MinValue)
			{
				var message = string.Format("Rating Value cannot be less than the minimum value ({0})", this.Rating.MinValue);
				yield return new ValidationResult(message, new List<string> {"RatingValue"});
			}

			if (this.RatingValue > this.Rating.MaxValue)
			{
				var message = string.Format("Rating Value cannot be greater than the maximum value ({0})", this.Rating.MaxValue);
				yield return new ValidationResult(message, new List<string> {"RatingValue"});
			}

		}
	}
}