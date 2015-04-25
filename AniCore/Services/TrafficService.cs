using System.Linq;
using Ani.Core.Models.Traffic;

namespace Ani.Core.Services
{
    public class TrafficService : Services.ServiceBase
    {
        private readonly string _apiKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceBase" /> class.
        /// </summary>
        /// <param name="entities">The database context for working with entity framework.</param>
        /// <param name="apiKey">The Bing Maps API key.</param>
        public TrafficService(Entities entities, string apiKey) : base(entities)
        {
            _apiKey = apiKey;
        }

        /// <summary>
        /// Gets the model for traffic information.
        /// </summary>
        /// <returns>The traffic model.</returns>
        public TrafficModel GetTrafficModel()
        {
            var model = new TrafficModel
            {
                Accidents = Entities.ActiveTrafficIncidentInfoSelect(true, false).ToList(),
                ConstructionEvents = Entities.ActiveTrafficIncidentInfoSelect(false, true).ToList(),
                BingMapsKey = _apiKey
            };

            return model;
        }
    }
}