using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static CVGS.Enumerations.CreditCardEnums;
using static CVGS.Enumerations.PersonEnums;

namespace CVGS.ViewModels
{
    [MetadataType(typeof(ProfileMetadata))]
    public partial class Profile
    {

    }

    public class ProfileMetadata
    {
        [Display(Name = "Username")]
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(10, ErrorMessage = "Username cannot have more than 10 characters.")]
        public string userName { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(20, ErrorMessage = "Password cannot have more than 20 characters.")]
        [RegularExpression("^(?=[ -~]*?[A-Z])(?=[ -~]*?[a-z])(?=[ -~]*?[0-9])(?=[ -~]*?[~¡!@#$%^&*-+¿?])[ -~]{1,20}$", ErrorMessage = "Password must have at least 1 upper case letter, 1 lower case letter, 1 digit and 1 special character.")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Confirmation password is required.")]
        [StringLength(20, ErrorMessage = "Confirmation password cannot have more than 20 characters.")]
        [DataType(DataType.Password)]
        [Compare("password", ErrorMessage = "Confirmation password must match password.")]
        public string confirmedPassword { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(30, ErrorMessage = "First name cannot have more than 30 characters.")]
        public string fName { get; set; }

        [Display(Name = "Middle Name")]
        [StringLength(30, ErrorMessage = "Middle name cannot have more than 30 characters.")]
        public string mName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(30, ErrorMessage = "Last name cannot have more than 30 characters.")]
        public string lName { get; set; }

        [Display(Name = "Date of Birth")]
        [Required(ErrorMessage = "Birthdate is required.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public System.DateTime dob { get; set; }

        [Display(Name = "Gender")]
        [Required(ErrorMessage = "Gender is required.")]
        [EnumDataType(typeof(Gender), ErrorMessage = "Gender is not valid.")]
        public string gender { get; set; }

        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone number is required.")]
        [StringLength(14, ErrorMessage = "Phone number cannot have more than 14 characters.")]
        [RegularExpression("\\D*([2-9]\\d{2})(\\D*)([2-9]\\d{2})(\\D*)(\\d{4})\\D*", ErrorMessage = "Phone number must be in a valid canadian format.")]
        public string phone { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "E-mail address is required.")]
        [StringLength(60, ErrorMessage = "E-mail address cannot have more than 60 characters.")]
        [RegularExpression("[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", ErrorMessage = "E-mail must be in a valid format.")]
        public string email { get; set; }

        [Display(Name = "Receive Promotional Emails")]
        public bool acceptEmails { get; set; }

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
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/yy}")]
        public System.DateTime expDate { get; set; }

        [Display(Name = "Card Type")]
        [Required(ErrorMessage = "Card type is required.")]
        [EnumDataType(typeof(CreditCardType), ErrorMessage = "Credit card type is not valid.")]
        public string creditCardType { get; set; }

        [Display(Name = "CVV")]
        [Required(ErrorMessage = "CVV is required.")]
        [StringLength(3, ErrorMessage = "CVV must have 3 digits.", MinimumLength = 3)]
        public string CVV { get; set; }

        [Display(Name = "Billing Street")]
        [Required(ErrorMessage = "Billing street is required.")]
        [StringLength(100, ErrorMessage = "Street cannot have more than 100 characters.")]
        public string billStreet { get; set; }

        [Display(Name = "Billing City")]
        [Required(ErrorMessage = "Billing city is required.")]
        [StringLength(50, ErrorMessage = "City cannot have more than 50 characters.")]
        public string billCity { get; set; }

        [Display(Name = "Billing Postal Code")]
        [Required(ErrorMessage = "Billing postal code is required.")]
        [StringLength(7, ErrorMessage = "Postal code cannot have more than 7 characters.")]
        [RegularExpression("^[ABCEGHJKLMNPRSTVXY]{1}\\d{1}[ABCEGHJKLMNPRSTVWXYZ]{1} *\\d{1}[ABCEGHJKLMNPRSTVWXYZ]{1}\\d{1}$", ErrorMessage = "Billing postal code must be in a valid canadian postal code format.")]
        public string billPostalCode { get; set; }

        [Display(Name = "Billing Province")]
        [Required(ErrorMessage = "Billing province code is required.")]
        public string billProvinceCode { get; set; }

        [Display(Name = "Shipping Street")]
        [Required(ErrorMessage = "Shipping street is required.")]
        [StringLength(100, ErrorMessage = "Shipping street cannot have more than 100 characters.")]
        public string shipStreet { get; set; }

        [Display(Name = "Shipping City")]
        [Required(ErrorMessage = "Shipping city is required.")]
        [StringLength(50, ErrorMessage = "Shipping city cannot have more than 50 characters.")]
        public string shipCity { get; set; }

        [Display(Name = "Shipping Postal Code")]
        [Required(ErrorMessage = "Shipping postal code is required.")]
        [StringLength(7, ErrorMessage = "Postal code cannot have more than 7 characters.")]
        [RegularExpression("^[ABCEGHJKLMNPRSTVXY]{1}\\d{1}[ABCEGHJKLMNPRSTVWXYZ]{1} *\\d{1}[ABCEGHJKLMNPRSTVWXYZ]{1}\\d{1}$", ErrorMessage = "Shipping postal code must be in a valid canadian postal code format.")]
        public string shipPostalCode { get; set; }

        [Display(Name = "Shipping Province")]
        [Required(ErrorMessage = "Shipping province code is required.")]
        public string shipProvinceCode { get; set; }
    }
}