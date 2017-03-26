using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CVGS.ViewModels
{
    public class PlatformList
    {
        public PlatformList()
        {
            selectedPlatforms = new List<string>();
        }

        public List<string> selectedPlatforms { get; set; }
    }
}