using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CVGS.ViewModels
{
    public class GenreList
    {
        public GenreList()
        {
            selectedGenres = new List<string>();
        }

        public List<string> selectedGenres { get; set; }
    }
}