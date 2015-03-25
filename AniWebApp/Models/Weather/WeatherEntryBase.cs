namespace AniWebApp.Models.Weather
{
    /// <summary>
    /// Base information common for weather models.
    /// </summary>
    public abstract class WeatherEntryBase
    {

        /// <summary>
        /// Gets or sets the low temperature.
        /// </summary>
        /// <value>The low temperature.</value>
        public int Low { get; set; }

        /// <summary>
        /// Gets or sets the high temperature.
        /// </summary>
        /// <value>The high temperature.</value>
        public int High { get; set; }

        /// <summary>
        /// Gets or sets the weather code.
        /// </summary>
        /// <value>The weather code.</value>
        public int WeatherCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the weather code.
        /// </summary>
        /// <value>The name of the weather code.</value>
        public string WeatherCodeName { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the severity identifier.
        /// </summary>
        /// <value>The severity identifier.</value>
        public int SeverityId { get; set; }

        /// <summary>
        /// Gets or sets the icon class.
        /// </summary>
        /// <value>The icon class.</value>
        public string IconClass { get; set; }
           
    }
}