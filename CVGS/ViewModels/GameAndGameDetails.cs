using CVGS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CVGS.ViewModels
{
    public partial class GameAndGameDetails
    {
        public GameAndGameDetails()
        {
            Reviews = new List<Review>();
        }

        public int gameDetailId { get; set; }
        public int gameId { get; set; }
        public string title { get; set; }
        public string platformCode { get; set; }
        public string genreCode { get; set; }
        public System.DateTime relDate { get; set; }
        public string desc { get; set; }
        public string cover { get; set; }
        public string publisher { get; set; }
        public Nullable<decimal> rateAVG { get; set; }
        public decimal price { get; set; }
        public bool phyCopy { get; set; }
        public Nullable<int> qoh { get; set; }
        public List<Review> Reviews { get; set; }
    }
}