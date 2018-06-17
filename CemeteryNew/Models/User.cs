using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CemeteryNew.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Login { get; set; }

        [Required]
        public Guid Password { get; set; }

        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}