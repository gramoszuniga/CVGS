using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CVGS.ViewModels
{
    [MetadataType(typeof(PasswordRecovererMetadata))]
    public partial class PasswordRecoverer
    {

    }

    public class PasswordRecovererMetadata
    {
        [Display(Name = "Username")]
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(10, ErrorMessage = "Username cannot have more than 10 characters.")]     
        public string userName { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "E-mail address is required.")]
        [StringLength(60, ErrorMessage = "E-mail address cannot have more than 60 characters.")]
        [RegularExpression("[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", ErrorMessage = "E-mail must be in a valid format.")]
        public string email { get; set; }
    }
}     