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
        /// <param name="zipCode">The zip code.</param>
        /// <returns>The traffic model.</returns>
        public TrafficModel GetTrafficModel(int zipCode)
        {
            var model = new TrafficModel
            {
                Accidents = Entities.ActiveTrafficIncidentInfoSelect(true, false).ToList(),
                ConstructionEvents = Entities.ActiveTrafficIncidentInfoSelect(false, true).ToList(),
                BingMapsKey = _apiKey
            };

            // Get the zip code, defaulting to the default zip code if none found
            var zip = Entities.ZipCodes.FirstOrDefault(z => z.ID == zipCode) ??
                      Entities.ZipCodes.FirstOrDefault(z => z.ID == 43035);

            // If we have a zip code, use it to inform our lat / longitude values
            if (zip != null)
            {
                if (zip.Lat != null)
                {
                    model.MapLat = zip.Lat.Value;
                }

                if (zip.Lng != null)
                {
                    model.MapLong = zip.Lng.Value;
                }
            }

            return model;
        }
    }
}