using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CVGS.Models
{
    //[MetadataType(typeof(OrderDetailMetadata))]
    public partial class OrderDetail
    {

    }

    public class OrderDetailMetadata
    {
        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, 999, ErrorMessage = "Quantity must be between 1 and 999.")]
        public int quantity { get; set; }

        [Required(ErrorMessage = "Unit price is required.")]
        [Range(0.00, 999.99, ErrorMessage = "Unit price must be between $0.00 and $999.99.")]
        public decimal uPrice { get; set; }
    }
}