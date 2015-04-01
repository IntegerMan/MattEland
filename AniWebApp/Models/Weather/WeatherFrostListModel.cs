using System.Collections.Generic;

namespace AniWebApp.Models.Weather
{
    /// <summary>
    /// A model used for displaying lists of frost information
    /// </summary>
    public class WeatherFrostListModel
    {

        /// <summary>
        /// Gets or sets a value indicating whether the user can add frost entries.
        /// </summary>
        /// <value><c>true</c> if the user can add frost entries; otherwise, <c>false</c>.</value>
        public bool CanAddFrostEntry { get; set; }

        /// <summary>
        /// Gets or sets the frost entries.
        /// </summary>
        /// <value>The frost entries.</value>
        public IList<WeatherFrostDataSelect_Result> Items { get; set; }
    }
}