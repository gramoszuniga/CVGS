using ExpressiveAnnotations.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static CVGS.Enumerations.PersonEnums;

namespace CVGS.Models
{
    //[MetadataType(typeof(PersonMetadata))]
    public partial class Person
    {

    }

    public class PersonMetadata
    {
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
        [AssertThat("dob <= Now()")]
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
        [Required(ErrorMessage = "E-mail is required.")]
        [StringLength(60, ErrorMessage = "E-mail cannot have more than 60 characters.")]
        [RegularExpression("[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", ErrorMessage = "E-mail must be in a valid format.")]
        public string email { get; set; }

        [Display(Name = "Registration Date")]
        [AssertThat("regDate == Today()")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public System.DateTime regDate { get; set; }
    }
}