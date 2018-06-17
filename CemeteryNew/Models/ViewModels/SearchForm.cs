using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CemeteryNew.Models.ViewModels
{
    public class SearchForm
    {
        [Display(Name ="Фамилия")]
        public string LName { get; set; }

        [Display(Name ="Имя")]
        public string FName { get; set; }

        [Display(Name ="Отчество")]
        public string SName { get; set; }

        [Display(Name ="Дата рождения")]
        public DateTime? DOB { get; set; }

        [Display(Name ="Дата смерти")]
        public DateTime? DateDeath { get; set; }

        [Display(Name ="Категория")]
        public string Category { get; set; }

        [Display(Name ="Показывать неподтвержденных")]
        public bool ShowUnknown { get; set; }
    }
}