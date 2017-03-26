﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CVGS.ViewModels
{
    public partial class AccountViewModel
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
    }                 
}