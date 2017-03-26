using ExpressiveAnnotations.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static CVGS.Enumerations.CreditCardEnums;

namespace CVGS.Models
{
    //[MetadataType(typeof(CreditCardMetadata))]
    public partial class CreditCard
    {

    }

    public class CreditCardMetadata
    {
        [Display(Name = "Card Number")]
        [Required(ErrorMessage = "Card number is required.")]
        [StringLength(16, ErrorMessage = "Card number must have 16 digits.", MinimumLength = 16)]
        public string number { get; set; }

        [Display(Name = "Name on Card")]
        [Required(ErrorMessage = "Name on card is required.")]
        [StringLength(60, ErrorMessage = "Name on card cannot have more than 60 characters.")]
        public string name { get; set; }

        [Display(Name = "Expiry Date")]
        [Required(ErrorMessage = "Expiry date is required.")]
        [AssertThat("expDate > Today()")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/yy}")]
        public System.DateTime expDate { get; set; }

        [Display(Name = "Credit Card Type")]
        [Required(ErrorMessage = "Credit card type is required.")]
        [EnumDataType(typeof(CreditCardType), ErrorMessage = "Credit card type is not valid.")]
        public string creditCardType { get; set; }

        [Display(Name = "CVV")]
        [Required(ErrorMessage = "CVV is required.")]
        [StringLength(3, ErrorMessage = "CVV must have 3 digits.", MinimumLength = 3)]
        public string CVV { get; set; }
    }
}