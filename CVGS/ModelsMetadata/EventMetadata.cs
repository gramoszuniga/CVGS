using ExpressiveAnnotations.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CVGS.Models
{
    //[MetadataType(typeof(EventMetadata))]
    public partial class Event
    {

    }

    public class EventMetadata
    {
        [Display(Name = "Event Title")]
        [Required(ErrorMessage = "Event title is required.")]
        [StringLength(60, ErrorMessage = "Event title cannot have more than 60 characters.")]
        public string title { get; set; }

        [Display(Name = "Start Date")]
        [Required(ErrorMessage = "Start date is required.")]
        [AssertThat("startDate >= Now()")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy h:mm tt}")]
        public System.DateTime startDate { get; set; }

        [Display(Name = "End Date")]
        [Required(ErrorMessage = "End date is required.")]
        [AssertThat("endDate >= startDate")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy h:mm tt}")]
        public System.DateTime endDate { get; set; }

        [Display(Name = "Registration Fee")]
        [Required(ErrorMessage = "Registration fee is required.")]
        [Range(0.00, 999.99, ErrorMessage = "Registration fee must be between $0.00 and $999.99.")]
        public decimal regFee { get; set; }

        [Display(Name = "Venue")]
        [Required(ErrorMessage = "Venue is required.")]
        [StringLength(100, ErrorMessage = "Venue cannot have more than 100 characters.")]
        public string venue { get; set; }

        [Display(Name = "Notes")]
        [Required(ErrorMessage = "Notes are required.")]
        [StringLength(255, ErrorMessage = "Notes cannot have more than 255 characters.")]
        public string notes { get; set; }
    }
}     