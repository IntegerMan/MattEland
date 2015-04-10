using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ani.Core.Models.Metrics
{
    public class UserRatingHistoryEntry : RatingModelBase
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public int UserId{ get; set; }

        [DisplayName("Created Time")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedTimeUTC { get; set; }

        [DisplayName("Modified Time")]
        [DataType(DataType.DateTime)]
        public DateTime ModifiedTimeUTC{ get; set; }


        public int Id{ get; set; }

        /// <summary>
        /// Gets the rating percent with 100 meaning 100% and 0 meaning 0%.
        /// </summary>
        /// <value>The rating percent.</value>
        public double RatingPercentWhole
        {
            get
            {
                // Protect against bad rating values during reflection / validation
                if (Rating == null)
                {
                    return 0;
                }

                int numRatings = (Rating.MaxValue - Rating.MinValue);
                double valuePerBump = 100.0/numRatings;

                return (RatingValue - Rating.MinValue) * valuePerBump;
            }
        }
    }
}