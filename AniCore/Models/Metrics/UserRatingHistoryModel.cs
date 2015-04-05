using System.Collections.Generic;
using Ani.Core.Models.Users;

namespace Ani.Core.Models.Metrics
{
    /// <summary>
    /// Information on a user's history with a particular rating
    /// </summary>
    public class UserRatingHistoryModel
    {
        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>The user.</value>
        public UserModel User { get; set; }

        /// <summary>
        /// Gets or sets the rating.
        /// </summary>
        /// <value>The rating.</value>
        public RatingModel Rating { get; set; }

        /// <summary>
        /// Gets or sets the history entries.
        /// </summary>
        /// <value>The history entries.</value>
        public List<UserRatingHistoryEntry> HistoryEntries { get; set; }
    }
}