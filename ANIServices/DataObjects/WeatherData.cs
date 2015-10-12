using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MattEland.Ani.AniServices.DataObjects
{
    /// <summary>
    /// Encapsulated Weather Data for current weather and upcoming forecasts
    /// </summary>
    [DataContract]
    public class WeatherData
    {

        /// <summary>
        /// Gets or sets the zip code.
        /// </summary>
        /// <value>The zip code.</value>
        [DataMember]
        public int ZipCode { get; set; }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>The latitude.</value>
        [DataMember]
        public double? Lat { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>The longitude.</value>
        [DataMember]
        public double? Long { get; set; }

        /// <summary>
        /// Gets or sets the name of the city.
        /// </summary>
        /// <value>The city.</value>
        [DataMember]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the pressure units.
        /// </summary>
        /// <value>The pressure units.</value>
        [DataMember]
        public string PressureUnits { get; set; }

        /// <summary>
        /// Gets or sets the visibility units.
        /// </summary>
        /// <value>The visibility units.</value>
        [DataMember]
        public string VisibilityUnits { get; set; }

        /// <summary>
        /// Gets or sets the wind units.
        /// </summary>
        /// <value>The wind units.</value>
        [DataMember]
        public string WindUnits { get; set; }

        /// <summary>
        /// Gets or sets the temperature units.
        /// </summary>
        /// <value>The temperature units.</value>
        [DataMember]
        public string TemperatureUnits { get; set; }

        /// <summary>
        /// Gets or sets the current weather conditions.
        /// </summary>
        /// <value>The conditions.</value>
        [DataMember]
        public WeatherConditionData Conditions { get; set; }

        /// <summary>
        /// Gets or sets the forecasts.
        /// </summary>
        /// <value>The forecasts.</value>
        [DataMember]
        public List<WeatherForecastData> Forecasts { get; set; }
    }
}