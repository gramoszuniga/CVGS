using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CVGS.Binders
{
    public class DateTimeBinder : IModelBinder

    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            bindingContext.ModelState.SetModelValue(bindingContext.ModelName, value);

            if (value == null)
            {
                return null;
            }
            return value.ConvertTo(typeof(DateTime), CultureInfo.CreateSpecificCulture("en-US"));
        }
    }
}