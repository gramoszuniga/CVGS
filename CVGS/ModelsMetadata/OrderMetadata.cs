using ExpressiveAnnotations.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static CVGS.Enumerations.OrderEnums;

namespace CVGS.Models
{
    //[MetadataType(typeof(OrderMetadata))]
    public partial class Order
    {

    }

    public class OrderMetadata
    {
        [Required(ErrorMessage = "Order date is required.")]
        [AssertThat("ordDate == Today()")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public System.DateTime ordDate { get; set; }

        [Required(ErrorMessage = "Order status is required.")]        
        [EnumDataType(typeof(OrderStatus), ErrorMessage = "Order status is not valid.")]
        public string status { get; set; }

        [Required(ErrorMessage = "Total is required.")]
        [Range(0.00, 999.99, ErrorMessage = "Total must be between $0.00 and $999.99.")]
        public decimal total { get; set; }
    }
}