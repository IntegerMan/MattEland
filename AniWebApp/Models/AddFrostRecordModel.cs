using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AniWebApp.Models
{
    /// <summary>
    /// A model used for recording observations on frost.
    /// </summary>
    public class AddFrostRecordModel
    {
        /// <summary>
        /// The zip code
        /// </summary>
        [DisplayName("Zip Code")]
        [DataType(DataType.PostalCode)]
        [Required]
        public int ZipCode { get; set; }

        /// <summary>
        /// The amount of time, in minutes, it took to defrost a car.
        /// </summary>
        [DisplayName("Actual Minutes")]
        [Required]
        public float ActualMinutes { get; set; }

        /// <summary>
        /// Whether or not it rained overnight
        /// </summary>
        [DisplayName("Rained Overnight?")]
        public bool RainedOvernight { get; set; }

        /// <summary>
        /// The date the observation was made
        /// </summary>
        [DisplayName("Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Required]
        public DateTime RecordDate { get; set; }
    }
}