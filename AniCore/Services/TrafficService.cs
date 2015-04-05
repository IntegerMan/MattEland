namespace Ani.Core.Services
{
    public class TrafficService : Services.ServiceBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceBase" /> class.
        /// </summary>
        /// <param name="entities">The database context for working with entity framework.</param>
        public TrafficService(Entities entities) : base(entities)
        {
        }
    }
}