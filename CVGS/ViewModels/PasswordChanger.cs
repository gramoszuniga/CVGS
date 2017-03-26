using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CVGS.ViewModels
{
    public partial class PasswordChanger
    {
        public string currentPassword { get; set; }
        public string newPassword { get; set; }
        public string confirmedNewPassword { get; set; }
    }
}