using System;

namespace AniWebApp.Models.Weather
{
    /// <summary>
    /// A model containing weather forecasts for a specific date.
    /// </summary>
    public class WeatherForecastModel : WeatherEntryBase
    {
        /// <summary>
        /// Gets or sets the low temperature in imperial units.
        /// </summary>
        /// <value>The low temperature.</value>
        public int Low { get; set; }

        /// <summary>
        /// Gets or sets the high temperature in imperial units.
        /// </summary>
        /// <value>The high temperature.</value>
        public int High { get; set; }

        /// <summary>
        /// Gets or sets the estimated minutes to defrost a car in the morning.
        /// </summary>
        /// <value>The minutes to defrost a car in the morning.</value>
        public double? MinutesToDefrost { get; set; }

        /// <summary>
        /// Gets or sets the forecast date in UTC.
        /// </summary>
        /// <value>The forecast date.</value>
        public DateTime ForecastDate { get; set; }

    }

}