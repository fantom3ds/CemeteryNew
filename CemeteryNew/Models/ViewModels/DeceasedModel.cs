using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CemeteryNew.Models.ViewModels
{
    public class DeceasedModel
    {
        public int Id { get; set; }

        [Display(Name ="Выбранное фото")]
        public string Photo { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Фамилия")]
        public string LName { get; set; }

        [MaxLength(20)]
        [Required]
        [Display(Name = "Имя")]
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

        [Display(Name = "Номер участка")]
        public int? NArea { get; set; } = 0;

        [Display(Name = "Номер могилы")]
        public int? NBur { get; set; } = 0;

        [Display(Name = "Описание")]
        [MaxLength(200)]
        public string Description { get; set; }

        [Display(Name = "Подтвержденное")]
        public bool Confirmed { get; set; } = false;

        public DeceasedModel(Deceased deceased)
        {
            Id = deceased.Id;
            FName = deceased.FName;
            LName = deceased.LName;
            SName = deceased.SName;
            DOB = deceased.DOB;
            DateDeath = deceased.DateDeath;
            Description = deceased.Description;
            Confirmed = deceased.Confirmed;
            Photo = deceased.Photo;
        }

        public DeceasedModel()
        {

        }
    }
}