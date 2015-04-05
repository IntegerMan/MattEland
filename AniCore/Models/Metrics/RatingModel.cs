namespace Ani.Core.Models.Metrics
{
    public class RatingModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        public System.DateTime CreatedDateUTC { get; set; }

        public bool IsActive { get; set; }

        public bool IsGlobal { get; set; }

        public string IconClass { get; set; }

        public int MinValue { get; set; }

        public int MaxValue { get; set; }

        public string Description { get; set; }

        public string MinLabel { get; set; }

        public string MaxLabel { get; set; }

        public bool RequireComments { get; set; }
    }
}