using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CemeteryNew.Models
{
    public class BurialPlace
    {
        public int Id { get; set; }

        [Display(Name ="Номер участка")]
        public int NArea { get; set; }

        [Display(Name ="Номер могилы")]
        public int NBurial { get; set; }

        public List<Deceased> Deceaseds { get; set; }
    }
}