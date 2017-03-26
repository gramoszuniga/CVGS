using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CVGS.Models
{
    //[MetadataType(typeof(CartDetailMetadata))]
    public partial class CartDetail
    {

    }

    public class CartDetailMetadata
    {
        [Range(1, 999, ErrorMessage = "Quantity must be between 0 and 999.")]
        public int quantity { get; set; }
    }
}