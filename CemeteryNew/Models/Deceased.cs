using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CemeteryNew.Models
{
    public class Deceased
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Фамилия")]
        public string LName { get; set; }

        [MaxLength(20)]
        [Required]
        [Display(Name ="Имя")]
        public string FName { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Отчество")]
        public string SName { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        [Display(Name = "Дата рождения")]
        public DateTime? DOB { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        [Display(Name = "Дата смерти")]
        public DateTime? DateDeath { get; set; }

        [Display(Name ="Категории")]
        public virtual List<Category> Categories { get; set; }

        [ForeignKey("BurialPlace")]
        public int? BurialPlaseId { get; set; }
        
        public virtual BurialPlace BurialPlace { get; set; }

        public string Photo { get; set; }

        [Display(Name ="Описание")]
        [MaxLength(200)]
        public string Description { get; set; }

        public bool Confirmed { get; set; } = false;

    }
}