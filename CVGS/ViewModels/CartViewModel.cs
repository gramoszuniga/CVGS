using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CVGS.ViewModels
{
    public partial class CartViewModel
    {
        public int cartDetailId { get; set; }
        public int personId { get; set; }
        public int gameDetaild { get; set; }
        public string title { get; set; }
        public int quantity { get; set; }
        public decimal price { get; set; }
        public decimal itemTotal { get; set; }
    }
}