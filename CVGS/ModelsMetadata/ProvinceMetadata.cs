using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CVGS.Models
{
    //[MetadataType(typeof(ProvinceMetadata))]
    public partial class Province
    {

    }

    public class ProvinceMetadata
    {
        [Required(ErrorMessage = "Province code is required.")]
        [StringLength(50, ErrorMessage = "Province code cannot have more than 50 characters.")]
        public string provinceCode { get; set; }

        [Required]
        [Range(0.00, 1.00, ErrorMessage = "Provincial tax must be between 0.00 and 1.00.")]
        public decimal provTax { get; set; }
    }
}