using System;
using System.Runtime.Serialization;

namespace MattEland.Ani.AniServices.DataObjects
{
    /// <summary>
    /// Data on the current weather conditions
    /// </summary>
    [DataContract]
    public class WeatherConditionData {

        /// <summary>
        /// Gets or sets the date the weather conditions was received.
        /// </summary>
        /// <value>The weather date.</value>
        [DataMember]
        public DateTime WeatherDate { get; set; }

        /// <summary>
        /// Gets or sets the weather code. This corresponds to Yahoo Weather's codes.
        /// </summary>
        /// <value>The weather code.</value>
        [DataMember]
        public int WeatherCode { get; set; }

        /// <summary>
        /// Gets or sets the weather severity identifier.
        /// </summary>
        /// <value>The weather severity identifier.</value>
        [DataMember]
        public int SeverityId { get; set; }

        /// <summary>
        /// Gets or sets the weather description.
        /// </summary>
        /// <value>The weather description.</value>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the temperature.
        /// </summary>
        /// <value>The temperature.</value>
        [DataMember]
        public int Temperature { get; set; }

        /// <summary>
        /// Gets or sets the windchill in degrees farenheight.
        /// </summary>
        /// <value>The windchill.</value>
        [DataMember]
        public int WindChill { get; set; }

        /// <summary>
        /// Gets or sets the wind speed in miles per hour.
        /// </summary>
        /// <value>The wind speed.</value>
        [DataMember]
        public int WindSpeed { get; set; }

        /// <summary>
        /// Gets or sets the wind direction in degrees.
        /// </summary>
        /// <value>The wind direction.</value>
        [DataMember]
        public int WindDirection { get; set; }

        /// <summary>
        /// Gets or sets the wind cardinal direction (e.g. NNE, SW).
        /// </summary>
        /// <value>The wind cardinal direction.</value>
        [DataMember]
        public string WindCardinalDirection { get; set; }

        /// <summary>
        /// Gets or sets the time of the sunrise.
        /// </summary>
        /// <value>The sunrise time.</value>
        [DataMember]
        public string Sunrise { get; set; }

        /// <summary>
        /// Gets or sets the time of the sunset.
        /// </summary>
        /// <value>The sunset.</value>
        [DataMember]
        public string Sunset { get; set; }

        /// <summary>
        /// Gets or sets the humidity percentage.
        /// </summary>
        /// <value>The humidity.</value>
        [DataMember]
        public int Humidity { get; set; }

        /// <summary>
        /// Gets or sets the visibility in miles.
        /// </summary>
        /// <value>The visibility.</value>
        [DataMember]
        public int Visibility { get; set; }

        /// <summary>
        /// Gets or sets the pressure in inches.
        /// </summary>
        /// <value>The pressure.</value>
        [DataMember]
        public double Pressure { get; set; }

        /// <summary>
        /// Gets or sets the rising. This is currently believed to equate to the UV Index.
        /// </summary>
        /// <value>The rising.</value>
        [DataMember]
        public int Rising { get; set; }

        /// <summary>
        /// Gets or sets the name of the weather code.
        /// </summary>
        /// <value>The name of the weather code.</value>
        [DataMember]
        public string WeatherCodeName { get; set; }

        /// <summary>
        /// Gets or sets the weather icon class for HTML representation.
        /// </summary>
        /// <value>The icon class.</value>
        [DataMember]
        public string IconClass { get; set; }
    }
}