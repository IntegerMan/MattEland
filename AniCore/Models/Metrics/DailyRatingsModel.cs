using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Ani.Core.Models.Users;

namespace Ani.Core.Models.Metrics
{
    public class DailyRatingsModel
    {
        
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public UserModel User { get; set; }

        public IEnumerable<RatingModel> Ratings { get; set; }

        public IEnumerable<UserRatingHistoryEntry> HistoryEntries { get; set; }
         
    }
}