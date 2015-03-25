namespace AniWebApp.Models.Weather
{
    /// <summary>
    /// A model representing the current weather conditions.
    /// </summary>
    public class WeatherConditionsModel : WeatherEntryBase
    {

        /// <summary>
        /// Gets or sets the current temperature in imperial units.
        /// </summary>
        /// <value>The temperature.</value>
        public int Temperature { get; set; }

        public string Sunrise { get; set; }
        public string Sunset { get; set; }
        public double Pressure { get; set; }
        public int Rising { get; set; }
        public int Visibility { get; set; }
        public int WindChill { get; set; }
        public int WindDirection { get; set; }
        public int WindSpeed { get; set; }
        public int Humidity { get; set; }
    }
}