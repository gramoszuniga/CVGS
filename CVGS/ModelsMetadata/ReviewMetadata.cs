using ExpressiveAnnotations.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static CVGS.Enumerations.ReviewEnums;

namespace CVGS.Models
{
    //[MetadataType(typeof(ReviewMetadata))]
    public partial class Review
    {

    }

    public class ReviewMetadata
    {
        [Required(ErrorMessage = "Rating is required.")]
        [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5.")]
        public int rating { get; set; }

        [Required(ErrorMessage = "Review status is required.")]
        [EnumDataType(typeof(ReviewStatus), ErrorMessage = "Review status is not valid.")]
        public string status { get; set; }

        [AssertThat("revDate == Today()")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public System.DateTime revDate { get; set; }

        [Required(ErrorMessage = "Review comment is required.")]
        [StringLength(255, ErrorMessage = "Review comment cannot have more than 255 characters.")]
        public string revComment { get; set; }
    }
}