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
            Role adminRole = new Role { Name = "admin" };
            Role userRole = new Role { Name = "user" };
            DB.Roles.AddOrUpdate(k => k.Name, adminRole, userRole);

            User admin = new User { Login = "Admin", Password = EncoderGuid.PasswordToGuid.Get("2033"), Role = adminRole };
            User user = new User { Login = "������", Password = EncoderGuid.PasswordToGuid.Get("1111"), Role = userRole };

            DB.Users.AddOrUpdate(u => u.Login, admin, user);

            DB.Categories.AddOrUpdate(c => c.CategoryName, 
                new Category { CategoryName = "<��� ���������>" },
                new Category { CategoryName = "�����������������" },
                new Category { CategoryName = "�������" },
                new Category { CategoryName = "�������" },
                new Category { CategoryName = "��������� ����� 1812 ����" }
                );

            DB.SaveChanges();

            base.Seed(DB);
        }
    }
}
