using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CemeteryNew.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Display(Name ="Категория")]
        [MaxLength(50)]
        public string CategoryName { get; set; }

        public List<Deceased> Deceaseds { get; set; }
    }
}