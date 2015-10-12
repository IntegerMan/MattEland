using System;
using System.Runtime.Serialization;

namespace MattEland.Ani.AniServices.DataObjects
{
    /// <summary>
    /// Information on a specific date's weather forecast
    /// </summary>
    [DataContract]
    public class WeatherForecastData
    {

        /// <summary>
        /// Gets or sets the date of the forecast.
        /// </summary>
        /// <value>The forecast date.</value>
        [DataMember]
        public DateTime ForecastDate { get; set; }

        /// <summary>
        /// Gets or sets the low temperature forecast.
        /// </summary>
        /// <value>The low.</value>
        [DataMember]
        public int Low { get; set; }

        /// <summary>
        /// Gets or sets the high temerature forecast.
        /// </summary>
        /// <value>The high temperature.</value>
        [DataMember]
        public int High { get; set; }

        /// <summary>
        /// Gets or sets the expected minutes to defrost.
        /// </summary>
        /// <value>The minutes to defrost.</value>
        [DataMember]
        public double? MinutesToDefrost { get; set; }

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
        /// Gets or sets the name of the weather code.
        /// </summary>
        /// <value>The name of the weather code.</value>
        [DataMember]
        public string WeatherCodeName { get; set; }

        /// <summary>
        /// Gets or sets the icon class.
        /// </summary>
        /// <value>The icon class.</value>
        [DataMember]
        public string IconClass { get; set; }
    }
}