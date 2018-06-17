using CemeteryNew.Models;
using CemeteryNew.Models.ViewModels;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;

namespace CemeteryNew.Controllers
{
    public class AccountController : Controller
    {
        #region Авторизация

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // поиск пользователя в бд
                    User user = null;
                    using (DataContext db = new DataContext())
                    {
                        Guid password = EncoderGuid.PasswordToGuid.Get(model.Password);
                        user = db.Users.FirstOrDefault(u => u.Login == model.Login && u.Password == password);
                        if (user != null)
                        {
                            FormsAuthentication.SetAuthCookie(model.Login, true);
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                        }
                    }
                }
                return View(model);
            }
            catch (Exception Ex)
            {
                return new HttpNotFoundResult(Ex.Message + "\n" + Ex.TargetSite + "\n" + Ex.InnerException);
            }
        }

        #endregion

        #region Регистрация

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Register model)
        {
            if (ModelState.IsValid)
            {
                User user = null;
                using (DataContext db = new DataContext())
                {
                    user = db.Users.FirstOrDefault(u => u.Login == model.Login);

                    if (user == null)
                    {
                        // создаем нового пользователя
                        db.Users.Add(new User { Login = model.Login, Password = EncoderGuid.PasswordToGuid.Get(model.Password), RoleId = 2 });
                        db.SaveChanges();

                        Guid password = EncoderGuid.PasswordToGuid.Get(model.Password);
                        user = db.Users.Where(u => u.Login == model.Login && u.Password == password).FirstOrDefault();

                        // если пользователь удачно добавлен в бд
                        if (user != null)
                        {
                            FormsAuthentication.SetAuthCookie(model.Login, true);
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                    }
                }
            }
            return View(model);
        }

        #endregion

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}