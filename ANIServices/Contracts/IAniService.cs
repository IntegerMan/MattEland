using System;
using System.ServiceModel;
using MattEland.Ani.AniServices.DataObjects;

namespace MattEland.Ani.AniServices.Contracts
{
    /// <summary>
    /// The public interface (service contract) of the ANI Data Service
    /// </summary>
    [ServiceContract]
    public interface IAniService
    {

        /// <summary>
        /// Gets the number of minutes estimated to scrape frost from a car for the specified zip code and date.
        /// This will return null if out of the area of service or not for a time with recorded data present.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="apiKey">The API key.</param>
        /// <param name="zipCode">The zip code.</param>
        /// <param name="predictionDate">The prediction date.</param>
        /// <returns>The number of minutes required to scrape frost from a car in the morning.</returns>
        [OperationContract]
        double? GetFrostScrapeTimeInMinutes(string userName, string apiKey, int zipCode, DateTime predictionDate);

        /// <summary>
        /// Gets weather data for a specified zip code including current conditions and forecasts.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="apiKey">The API key.</param>
        /// <param name="zipCode">The zip code.</param>
        /// <returns>Weather data for the specified zip code.</returns>
        [OperationContract]
        WeatherData GetWeatherData(string userName, string apiKey, int zipCode);

    }
}
