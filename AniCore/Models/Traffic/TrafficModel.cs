using System.Collections.Generic;

namespace Ani.Core.Models.Traffic
{
    public class TrafficModel
    {
        public List<ActiveTrafficIncidentInfoSelect_Result> Accidents { get; set; }
        public List<ActiveTrafficIncidentInfoSelect_Result> ConstructionEvents { get; set; }

        /// <summary>
        /// Gets or sets the Bing Maps API key.
        /// </summary>
        /// <value>The bing maps key.</value>
        public string BingMapsKey { get; set; }

        /// <summary>
        /// Gets or sets the map latitude.
        /// </summary>
        /// <value>The map latitude.</value>
        public double MapLat { get; set; }

        /// <summary>
        /// Gets or sets the map longitude.
        /// </summary>
        /// <value>The map longitude.</value>
        public double MapLong { get; set; }
    }
}