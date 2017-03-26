using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CVGS.ViewModels
{
    [MetadataType(typeof(PasswordChangerMetadata))]
    public partial class PasswordChanger
    {

    }

    public class PasswordChangerMetadata
    {
        [Display(Name = "Current Password")]
        [Required(ErrorMessage = "Current password is required.")]
        [StringLength(20, ErrorMessage = "Current password cannot have more than 20 characters.")]
        [RegularExpression("^(?=[ -~]*?[A-Z])(?=[ -~]*?[a-z])(?=[ -~]*?[0-9])(?=[ -~]*?[~¡!@#$%^&*-+¿?])[ -~]{1,20}$", ErrorMessage = "Current password must have at least 1 upper case letter, 1 lower case letter, 1 digit and 1 special character.")]
        [DataType(DataType.Password)]
        public string currentPassword { get; set; }

        [Display(Name = "New Password")]
        [Required(ErrorMessage = "New password is required.")]
        [StringLength(20, ErrorMessage = "New password cannot have more than 20 characters.")]
        [RegularExpression("^(?=[ -~]*?[A-Z])(?=[ -~]*?[a-z])(?=[ -~]*?[0-9])(?=[ -~]*?[~¡!@#$%^&*-+¿?])[ -~]{1,20}$", ErrorMessage = "New password must have at least 1 upper case letter, 1 lower case letter, 1 digit and 1 special character.")]
        [DataType(DataType.Password)]
        public string newPassword { get; set; }

        [Display(Name = "Reenter New Password")]
        [Required(ErrorMessage = "New confirmation password is required.")]
        [StringLength(20, ErrorMessage = "New confirmation password cannot have more than 20 characters.")]
        [RegularExpression("^(?=[ -~]*?[A-Z])(?=[ -~]*?[a-z])(?=[ -~]*?[0-9])(?=[ -~]*?[~¡!@#$%^&*-+¿?])[ -~]{1,20}$", ErrorMessage = "New confirmation password must have at least 1 upper case letter, 1 lower case letter, 1 digit and 1 special character.")]
        [DataType(DataType.Password)]
        [Compare("newPassword", ErrorMessage = "Confirmation password must match new password.")]
        public string confirmedNewPassword { get; set; }
    }
}