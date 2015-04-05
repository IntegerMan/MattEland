using System;

namespace Ani.Core.Models.Weather
{
    /// <summary>
    /// A model representing the current weather conditions.
    /// </summary>
    public class WeatherConditionsModel : WeatherEntryBase
    {
        /// <summary>
        /// Gets or sets the date the weather conditions was received.
        /// </summary>
        /// <value>The weather date.</value>
        public DateTime WeatherDate { get; set; }

        /// <summary>
        /// Gets or sets the temperature.
        /// </summary>
        /// <value>The temperature.</value>
        public int Temperature { get; set; }

        /// <summary>
        /// Gets or sets the windchill in degrees farenheight.
        /// </summary>
        /// <value>The windchill.</value>
        public int WindChill { get; set; }

        /// <summary>
        /// Gets or sets the wind speed in miles per hour.
        /// </summary>
        /// <value>The wind speed.</value>
        public int WindSpeed { get; set; }

        /// <summary>
        /// Gets or sets the wind direction in degrees.
        /// </summary>
        /// <value>The wind direction.</value>
        public int WindDirection { get; set; }

        /// <summary>
        /// Gets or sets the wind cardinal direction (e.g. NNE, SW).
        /// </summary>
        /// <value>The wind cardinal direction.</value>
        public string WindCardinalDirection { get; set; }

        /// <summary>
        /// Gets or sets the time of the sunrise.
        /// </summary>
        /// <value>The sunrise time.</value>
        public string Sunrise { get; set; }

        /// <summary>
        /// Gets or sets the time of the sunset.
        /// </summary>
        /// <value>The sunset.</value>
        public string Sunset { get; set; }

        /// <summary>
        /// Gets or sets the humidity percentage.
        /// </summary>
        /// <value>The humidity.</value>
        public int Humidity { get; set; }

        /// <summary>
        /// Gets or sets the visibility in miles.
        /// </summary>
        /// <value>The visibility.</value>
        public int Visibility { get; set; }

        /// <summary>
        /// Gets or sets the pressure in inches.
        /// </summary>
        /// <value>The pressure.</value>
        public double Pressure { get; set; }

        /// <summary>
        /// Gets or sets the rising. This is currently believed to equate to the UV Index.
        /// </summary>
        /// <value>The rising.</value>
        public int Rising { get; set; }

    }
}