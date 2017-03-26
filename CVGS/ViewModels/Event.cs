using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CVGS.ViewModels
{
    public class Event
    {
        public int eventId { get; set; }
        public string title { get; set; }
        public System.DateTime startDate { get; set; }
        public System.DateTime endDate { get; set; }
        public decimal regFee { get; set; }      
        public bool isJoined { get; set; }
    }
}       