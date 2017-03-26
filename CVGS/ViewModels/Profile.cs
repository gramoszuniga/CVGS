using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CVGS.ViewModels
{
    public partial class Profile
    {
        public int personId { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string confirmedPassword { get; set; }
        public string fName { get; set; }
        public string mName { get; set; }
        public string lName { get; set; }
        public System.DateTime dob { get; set; }
        public string gender { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public bool acceptEmails { get; set; }
        public string number { get; set; }
        public string name { get; set; }
        public System.DateTime expDate { get; set; }
        public string creditCardType { get; set; }
        public string CVV { get; set; }
        public string billStreet { get; set; }
        public string billCity { get; set; }
        public string billPostalCode { get; set; }
        public string billProvinceCode { get; set; }
        public string shipStreet { get; set; }
        public string shipCity { get; set; }
        public string shipPostalCode { get; set; }
        public string shipProvinceCode { get; set; }
    }
}