using CemeteryNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Data.Entity;

namespace CemeteryNew.DataAccessLayer
{
    public class UserDal
    {
        DataContext DB = new DataContext();

        /// <summary>
        /// Возвращает список всех ролей для заданного пользователя
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public string[] GetRolesForUser(string username)
        {
            string[] roles = new string[] { };
            using (DataContext db = new DataContext())
            {
                string roleName = db.Users.Where(u => u.Login == username).Select(u => u.Role.Name).FirstOrDefault();
                if (roleName != "")
                {
                    // получаем роль
                    roles = new string[] { roleName };
                }
            }
            return roles;
        }

        /// <summary>
        /// Определяет, принадллежит ли заданный пользовать к заданной роли
        /// </summary>
        /// <param name="username"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public bool IsUserInRole(string username, string roleName)
        {
            // Получаем пользователя
            string roleName1 = DB.Users.Where(u => u.Login == username).Select(u => u.Role.Name).FirstOrDefault();
            if (roleName1 == roleName)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Возращает объект пользователя из базы по его логину
        /// </summary>
        /// <param name="username">Логин пользователя</param>
        /// <param name="allData">Загрузить роли</param>
        /// <returns></returns>
        public User GetUser(string username, bool allData = false)
        {
            User user = null;
            if (allData)
                user = DB.Users.Include(c => c.Role).FirstOrDefault(c => c.Login == username);
            else
                user = DB.Users.FirstOrDefault(c => c.Login == username);
            return user;
        }

        /// <summary>
        /// Возращает объект пользователя из базы по его Id
        /// </summary>
        /// <param name="username">Логин пользователя</param>
        /// <param name="allData">Загрузить роли</param>
        /// <returns></returns>
        public User GetUser(int Id, bool allData = false)
        {
            User user = null;
            if (allData)
                user = DB.Users.Include(c => c.Role).FirstOrDefault(c => c.Id == Id);
            else
                user = DB.Users.FirstOrDefault(c => c.Id == Id);
            return user;
        }

        public void EditUser(User user)
        {
            User old = DB.Users.Find(user.Id);
            old.Password = user.Password;
            DB.SaveChanges();
        }
    }
}