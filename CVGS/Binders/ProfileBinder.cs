using CVGS.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CVGS.Binders
{
    public class ProfileBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var request = controllerContext.HttpContext.Request;
            string userName = request.Form.Get("userName");
            string password = request.Form.Get("password");
            string confirmedPassword = request.Form.Get("confirmedPassword");
            string fName = request.Form.Get("fName");
            string mName = request.Form.Get("mName") == "" ? null : request.Form.Get("mName");
            string lName = request.Form.Get("lName");
            DateTime dob = new DateTime(int.Parse(request.Form.Get("dob").Split('/').ElementAt(2)), int.Parse(request.Form.Get("dob").Split('/').ElementAt(0)), int.Parse(request.Form.Get("dob").Split('/').ElementAt(1)));
            string gender = request.Form.Get("gender");
            string phone = request.Form.Get("phone");
            string email = request.Form.Get("email");
            ValueProviderResult value = bindingContext.ValueProvider.GetValue("acceptEmails");
            bool acceptEmails = (bool)value.ConvertTo(typeof(bool));
            string number = request.Form.Get("number");
            string name = request.Form.Get("name");
            DateTime expDate = new DateTime(DateTime.ParseExact(request.Form.Get("expDate"), "MM/yy", CultureInfo.CreateSpecificCulture("en-CA")).Year, int.Parse(request.Form.Get("expDate").Split('/').ElementAt(0)), 1);
            expDate = expDate.AddMonths(1);
            expDate = expDate.AddDays(-1);
            string creditCardType = request.Form.Get("creditCardType");
            string CVV = request.Form.Get("CVV");
            string billStreet = request.Form.Get("billStreet");
            string billCity = request.Form.Get("billCity");
            string billPostalCode = request.Form.Get("billPostalCode");
            string billProvinceCode = request.Form.Get("billProvinceCode");
            string shipStreet = request.Form.Get("shipStreet");
            string shipCity = request.Form.Get("shipCity");
            string shipPostalCode = request.Form.Get("shipPostalCode");
            string shipProvinceCode = request.Form.Get("shipProvinceCode");
            return new Profile
            {
                userName = userName,
                password = password,
                confirmedPassword = confirmedPassword,
                fName = fName,
                mName = mName,
                lName = lName,
                dob = dob,
                gender = gender,
                phone = phone,
                email = email,
                acceptEmails = acceptEmails,
                number = number,
                name = name,
                expDate = expDate,
                creditCardType = creditCardType,
                CVV = CVV,
                billStreet = billStreet,
                billCity = billCity,
                billPostalCode = billPostalCode,
                billProvinceCode = billProvinceCode,
                shipStreet = shipStreet,
                shipCity = shipCity,
                shipPostalCode = shipPostalCode,
                shipProvinceCode = shipProvinceCode
            };
        }
    }
}