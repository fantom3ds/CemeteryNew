using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CemeteryNew.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Display(Name ="Категория")]
        [MaxLength(20)]
        public string CategoryName { get; set; }

        public List<Deceased> Deceaseds { get; set; }
    }
}