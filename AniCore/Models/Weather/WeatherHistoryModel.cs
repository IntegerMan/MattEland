using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ani.Core.Models.Weather
{
    public class WeatherHistoryModel
    {
        /// <summary>
        /// The zip code
        /// </summary>
        public int ZipCode { get; set; }

        /// <summary>
        /// A list of weather predictions
        /// </summary>
        public IList<WeatherPrediction> Predictions { get; set; } = new List<WeatherPrediction>();

        /// <summary>
        /// Forecast chart data for low temperatures
        /// </summary>
        public string ForecastLowLineData;

        /// <summary>
        /// Forecast chart data for high temperatures
        /// </summary>
        public string ForecastHighLineData;
    }
}
