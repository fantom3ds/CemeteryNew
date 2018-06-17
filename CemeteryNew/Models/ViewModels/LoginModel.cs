using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CemeteryNew.Models.ViewModels
{
    public class LoginModel
    {
        [Required]
        [Display(Name ="Логин")]
        public string Login { get; set; }

        [Display(Name ="Пароль")]
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
    }
}