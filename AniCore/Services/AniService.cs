using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ani.Core.Models.Geo;

namespace Ani.Core.Services
{
    /// <summary>
    /// A service for interacting with generic ANI application functionality.
    /// </summary>
    public class AniService : ServiceBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceBase" /> class.
        /// </summary>
        /// <param name="entities">The database context for working with entity framework.</param>
        public AniService(Entities entities) : base(entities)
        {
        }

        /// <summary>
        /// Gets a collection zip codes that are currently in service.
        /// Zip codes are ordered numerically.
        /// </summary>
        /// <returns>A yielded collection of zip codes that are in service.</returns>
        public IEnumerable<ZipCodeModel> GetZipCodesInService()
        {
            var activeZipCodes = this.Entities.ZipCodes.Where(z => z.ServiceStatu.IsActive).OrderBy(z => z.ID);

            // Convert each entity into a model and yield that back
            foreach (var zipEntity in activeZipCodes)
            {
                yield return GetZipCodeModelFromEntity(zipEntity);
            }
        }

        /// <summary>
        /// Gets the zip code model from a zip code entity.
        /// </summary>
        /// <param name="zipCodeEntity">The zip code entity.</param>
        /// <returns>ZipCodeModel.</returns>
        public static ZipCodeModel GetZipCodeModelFromEntity(ZipCode zipCodeEntity)
        {
            var zipCode = new ZipCodeModel
            {
                ZipCode = zipCodeEntity.ID,
                ServiceStatus = zipCodeEntity.ServiceStatusID,
                CreatedTimeUtc = zipCodeEntity.CreatedDateUTC,
                Lat = zipCodeEntity.Lat,
                Long = zipCodeEntity.Lng,
                Name = zipCodeEntity.Name,
                State = zipCodeEntity.State
            };
            return zipCode;
        }
    }
}