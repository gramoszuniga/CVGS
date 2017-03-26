using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CVGS.Models
{
    //[MetadataType(typeof(CartMetadata))]
    public partial class Cart
    {

    }

    public class CartMetadata
    {
        [Range(0.00, 999.99, ErrorMessage = "Total must be between $0.00 and $999.99.")]
        public decimal total { get; set; }
    }
}