using System;

namespace Ani.Core.Models.Metrics
{
    public class UserRatingHistoryEntry
    {
        public int RatingId { get; set; }

        public int UserId{ get; set; }

        public int RatingValue { get; set; }

        public DateTime EntryDateUTC { get; set; }

        public DateTime CreatedTimeUTC { get; set; }

        public DateTime ModifiedTimeUTC{ get; set; }

        public string Comments{ get; set; }

        public int Id{ get; set; }
    }
}