using ExpressiveAnnotations.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static CVGS.Enumerations.GameEnums;

namespace CVGS.Models
{
    //[MetadataType(typeof(GameMetadata))]
    public partial class Game
    {

    }

    public class GameMetadata
    {
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(60, ErrorMessage = "Title cannot have more than 60 characters.")]
        public string title { get; set; }

        [Required(ErrorMessage = "Release date is required.")]
        [AssertThat("relDate <= Today()")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public System.DateTime relDate { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(255, ErrorMessage = "Description cannot have more than 255 characters.")]
        public string desc { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Cover cannot have more than 100 characters.")]
        public string cover { get; set; }
        [Required(ErrorMessage = "Publisher is required.")]

        [EnumDataType(typeof(Publisher), ErrorMessage = "Publisher is not valid.")]
        [StringLength(30, ErrorMessage = "Publisher cannot have more than 30 characters.")]
        public string publisher { get; set; }

        [Range(0.00, 5.00, ErrorMessage = "Average rating must be between 0.00 and 5.00.")]
        public Nullable<decimal> rateAVG { get; set; }
    }
}