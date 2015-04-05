using System.Collections.Generic;

namespace Ani.Core.Models.Metrics
{
    /// <summary>
    /// Contains information on available ratings for a user as well as their last rating information for each item.
    /// </summary>
    public class RatingsSummaryModel
    {
        public List<RatingsWithLatestInfoForUserSelect_Result> Ratings { get; set; }
    }
}