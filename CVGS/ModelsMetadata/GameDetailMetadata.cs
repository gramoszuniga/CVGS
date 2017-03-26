using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CVGS.Models
{
    //[MetadataType(typeof(GameDetailMetadata))]
    public partial class GameDetail
    {

    }

    public class GameDetailMetadata
    {
        [Required(ErrorMessage = "Price is required.")]
        [Range(0.00, 999.99, ErrorMessage = "Price must be between $0.00 and $999.99.")]
        public decimal price { get; set; }

        [Range(0, 999, ErrorMessage = "Quantity on hand must be between 0 and 999.")]
        public Nullable<int> qoh { get; set; }
    }
}