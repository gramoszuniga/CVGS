using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CVGS.Models
{
    //[MetadataType(typeof(GenreMetadata))]
    public partial class Genre
    {

    }

    public class GenreMetadata
    {
        [Required(ErrorMessage = "Genre is required.")]
        [StringLength(30, ErrorMessage = "Genre cannot have more than 30 characters.")]
        public string genreCode { get; set; }
    }
}