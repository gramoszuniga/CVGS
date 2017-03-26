using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CVGS.Models
{
    //[MetadataType(typeof(AddressMetadata))]
    public partial class Address
    {

    }

    public class AddressMetadata
    {
        [Display(Name = "Street Address")]
        [Required(ErrorMessage = "Street is required.")]
        [StringLength(100, ErrorMessage = "Street cannot have more than 100 characters.")]
        public string street { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "City is required.")]
        [StringLength(50, ErrorMessage = "City cannot have more than 50 characters.")]
        public string city { get; set; }

        [Display(Name = "Postal Code")]
        [Required(ErrorMessage = "Postal code is required.")]
        [StringLength(50, ErrorMessage = "Postal code cannot have more than 7 characters.")]
        [RegularExpression("^[ABCEGHJKLMNPRSTVXY]{1}\\d{1}[ABCEGHJKLMNPRSTVWXYZ]{1} *\\d{1}[ABCEGHJKLMNPRSTVWXYZ]{1}\\d{1}$", ErrorMessage = "Postal code must be in a valid canadian postal code format.")]
        public string postalCode { get; set; }

        [Display(Name = "Province")]
        [Required(ErrorMessage = "Province code is required.")]
        public string provinceCode { get; set; }
    }
}     