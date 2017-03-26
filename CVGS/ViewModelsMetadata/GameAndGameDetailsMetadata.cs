using ExpressiveAnnotations.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static CVGS.Enumerations.GameEnums;

namespace CVGS.ViewModels
{
    [MetadataType(typeof(GameAndGameDetailsMetadata))]
    public partial class GameAndGameDetails
    {

    }

    public class GameAndGameDetailsMetadata
    {
        [Display(Name = "ID")]
        public int gameId { get; set; }

        [Display(Name = "ID")]
        public int gameDetailId { get; set; }

        [Display(Name = "Game Title")]
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(60, ErrorMessage = "Title cannot have more than 60 characters.")]
        public string title { get; set; }

        [Display(Name = "Platform")]
        [Required(ErrorMessage = "Platform is required.")]
        public string platformCode { get; set; }

        [Display(Name = "Genre")]
        [Required(ErrorMessage = "Genre is required.")]
        public string genreCode { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "Price is required.")]
        [Range(0.00, 999.99, ErrorMessage = "Price must be between $0.00 and $999.99.")]
        public decimal price { get; set; }

        [Display(Name = "Quantity")]
        [Range(0, 999, ErrorMessage = "Quantity on hand must be between 0 and 999.")]
        public Nullable<int> qoh { get; set; }

        [Display(Name = "Physical Copy")]
        public bool phyCopy { get; set; }

        [Display(Name = "Average Rating")]
        [Range(0.00, 5.00, ErrorMessage = "Average rating must be between 0.00 and 5.00.")]
        public Nullable<decimal> rateAVG { get; set; }

        [Display(Name = "Release Date")]
        [Required(ErrorMessage = "Release date is required.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public System.DateTime relDate { get; set; }

        [Display(Name = "Publisher")]
        [Required(ErrorMessage = "Publisher is required.")]
        [EnumDataType(typeof(Publisher), ErrorMessage = "Publisher is not valid.")]
        [StringLength(30, ErrorMessage = "Publisher cannot have more than 30 characters.")]
        public string publisher { get; set; }

        [Display(Name = "Cover")]
        [StringLength(100, ErrorMessage = "Cover cannot have more than 100 characters.")]
        public string cover { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is required.")]
        [StringLength(255, ErrorMessage = "Description cannot have more than 255 characters.")]
        public string desc { get; set; }
    }
}