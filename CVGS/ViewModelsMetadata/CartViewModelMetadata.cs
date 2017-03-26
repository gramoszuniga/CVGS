using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CVGS.ViewModels
{
    [MetadataType(typeof(CartViewModelMetadata))]
    public partial class CartViewModel
    {

    }

    public class CartViewModelMetadata
    {
        public int cartDetailId { get; set; }
        public int personId { get; set; }
        public int gameDetaild { get; set; }
        [Display(Name = "Game Title")]
        public string title { get; set; }
        [Display(Name = "Quantity")]
        public int quantity { get; set; }
        [Display(Name = "Price")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:C2}")]
        public decimal price { get; set; }
        [Display(Name = "Item Total")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:C2}")]
        public decimal itemTotal { get; set; }
    }
}