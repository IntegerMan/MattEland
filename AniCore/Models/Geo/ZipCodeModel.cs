using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ani.Core.Models.Geo
{
    /// <summary>
    /// Represents a serviceable zip code
    /// </summary>
    public class ZipCodeModel
    {
        /// <summary>
        /// Gets or sets the zip code.
        /// </summary>
        /// <value>The zip code.</value>
        public int ZipCode { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// This can be null.
        /// </summary>
        /// <value>The state.</value>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the latitude.
        /// This can be null.
        /// </summary>
        /// <value>The latitude.</value>
        public double? Lat { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>The longitude.</value>
        public double? Long { get; set; }

        /// <summary>
        /// Gets or sets the service status of this zip code.
        /// </summary>
        /// <value>The service status.</value>
        public int ServiceStatus { get; set; }

        /// <summary>
        /// Gets or sets the created time in UTC.
        /// </summary>
        /// <value>The created time in UTC.</value>
        public DateTime CreatedTimeUtc { get; set; }
    }
}
