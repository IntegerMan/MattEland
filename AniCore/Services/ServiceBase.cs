namespace Ani.Core.Services
{
    /// <summary>
    /// Base logic shared by all service classes.
    /// </summary>
    public abstract class ServiceBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceBase"/> class.
        /// </summary>
        /// <param name="entities">The database context for working with entity framework.</param>
        protected ServiceBase(Entities entities)
        {
            Entities = entities;
        }

        /// <summary>
        /// Gets the database context for working with entity framework.
        /// </summary>
        /// <value>The database context for working with entity framework.</value>
        protected Entities Entities { get; }
    }
}