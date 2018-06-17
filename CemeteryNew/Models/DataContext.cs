using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CemeteryNew.Models
{
    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {
        }

        public DbSet<Deceased> Deceaseds { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BurialPlace> BurialPlaces { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}