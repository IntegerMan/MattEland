namespace Ani.Core.Services
{
    public class WeatherService : Services.ServiceBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceBase" /> class.
        /// </summary>
        /// <param name="entities">The database context for working with entity framework.</param>
        public WeatherService(Entities entities) : base(entities)
        {
        }
    }
}