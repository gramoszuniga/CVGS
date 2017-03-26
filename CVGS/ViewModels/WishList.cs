using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CVGS.ViewModels
{
    public class WishList
    {
        public int wishlistId { get; set; }
        public int gameId { get; set; }
        public string title { get; set; }
        public decimal phyPrice { get; set; }
        public decimal downPrice { get; set; }
    }
}