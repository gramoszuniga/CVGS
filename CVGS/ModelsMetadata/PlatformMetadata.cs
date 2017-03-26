using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CVGS.Models
{
    //[MetadataType(typeof(PlatformMetadata))]
    public partial class Platform
    {

    }

    public class PlatformMetadata
    {
        [Required(ErrorMessage = "Platform is required.")]
        [StringLength(30, ErrorMessage = "Platform cannot have more than 30 characters.")]
        public string platformCode { get; set; }
    }
}