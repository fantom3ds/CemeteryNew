namespace CemeteryNew.Migrations
{
    using CemeteryNew.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CemeteryNew.Models.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(CemeteryNew.Models.DataContext DB)
        {
            Role adminRole = new Role { Id=1, Name = "admin" };
            Role userRole = new Role { Id=2, Name = "user" };
            DB.Roles.AddOrUpdate(k => k.Name, adminRole, userRole);

            User admin = new User { Login = "Admin", Password = EncoderGuid.PasswordToGuid.Get("2033"), Role = adminRole };
            User user = new User { Id = 2, Login = "Даниил", Password = EncoderGuid.PasswordToGuid.Get("1111"), Role = userRole };

            DB.Users.AddOrUpdate(u => u.Login, admin, user);
            DB.SaveChanges();

            base.Seed(DB);
        }
    }
}
