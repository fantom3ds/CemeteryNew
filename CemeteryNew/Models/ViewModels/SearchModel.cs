using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CemeteryNew.Models.ViewModels
{
    public class SearchModel
    {
        public User User { get; set; }

        public SearchForm Form { get; set; }

        public List<Deceased> Deceaseds { get; set; }

        public SearchModel()
        {
            Deceaseds = new List<Deceased>();
            Form = new SearchForm();
        }
    }
}