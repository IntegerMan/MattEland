using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ani.Core.Models
{
    /// <summary>
    ///     A data model for searching.
    /// </summary>
    public sealed class SearchModel
    {
        /// <summary>
        ///     Gets or sets the search string.
        /// </summary>
        /// <value>
        ///     The search string.
        /// </value>
        [DisplayName("Search Query")]
        public string Query { get; set; }
    }
}