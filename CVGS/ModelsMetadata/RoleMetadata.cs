using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CVGS.Models
{
    //[MetadataType(typeof(RoleMetadata))]
    public partial class Role
    {

    }

    public class RoleMetadata
    {
        [Required(ErrorMessage = "Role code is required.")]
        [StringLength(40, ErrorMessage = "Role code cannot have more than 40 characters.")]
        public string roleCode { get; set; }

        [Range(0.00, 1.00, ErrorMessage = "Discount percentage must be between 0.00 and 1.00.")]
        public Nullable<decimal> disPct { get; set; }
    }
}