using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using CemeteryNew.Models;
using CemeteryNew.Models.ViewModels;
using System.Threading.Tasks;
using CemeteryNew.DataAccessLayer;

namespace CemeteryNew.Controllers
{
    public class HomeController : Controller
    {
        string fileName; // переменная путь файла
        UserDal userDal;
        DeceasedDal deceasedDal;

        public HomeController()
        {
            userDal = new UserDal();
            deceasedDal = new DeceasedDal();
        }

        #region Обычные страницы

        public ActionResult Index()
        {
            return View();
        }//главная страница

        public ActionResult Map()
        {
            return View();
        }//Страница карта

        public ActionResult Contact()
        {
            return View();
        }//Страница контакт

        public ActionResult Article()
        {
            return View();
        }//Страница статьи

        public ActionResult History()
        {
            return View();
        }

        #endregion

        #region Search (бэкап)

        //public ActionResult Search(List<Deceased> deceaseds)
        //{
        //    SearchModel search = new SearchModel();
        //    using (DataContext DB = new DataContext())
        //    {
        //        search.Deceaseds = new List<Deceased>(DB.Deceaseds.Where(c => c.Confirmed == true).Include(c => c.Categories).Include(a => a.BurialPlace));
        //    }
        //    return View(search);
        //}

        #endregion

        public async Task<ActionResult> Search(string searchString)
        {
            var deceaseds = deceasedDal.GetAllDeceased();
            if (!String.IsNullOrEmpty(searchString))
            {
                deceaseds = deceaseds.Where(s => s.LName.Contains(searchString) || s.FName.Contains(searchString) || s.SName.Contains(searchString));
            }
            return View(await deceaseds.ToListAsync());
        }

        public ActionResult DeceasedDetails(int Id)
        {
            Deceased deceased = deceasedDal.GetDeceased(Id);
            return View(deceased);
        }

        #region Личный кабинет

        public ActionResult PrivateOffice()
        {
            User principal = userDal.GetUser(User.Identity.Name, true);
            if (principal == null)
                return
                    new HttpNotFoundResult("Ошибка входа в личный кабинет, перелогиньтесь и попробуйте снова");
            return View(principal);
        }

        public ActionResult EditUser(int Id)
        {
            User principal = userDal.GetUser(Id);
            if (principal == null)
                return
                    new HttpNotFoundResult("Ошибка входа в личный кабинет, перелогиньтесь и попробуйте снова");
            EditUser a = new EditUser { Id = principal.Id };
            return View(a);
        }

        [HttpPost]
        [ActionName("EditUser")]
        public ActionResult EditedUser(EditUser edit)
        {
            User principal = userDal.GetUser(edit.Id);
            if (principal == null)
                return
                    new HttpNotFoundResult("Пользователь не найден, перелогиньтесь и попробуйте снова");

            principal.Password = EncoderGuid.PasswordToGuid.Get(edit.Password);
            userDal.EditUser(principal);
            return RedirectToAction("PrivateOffice", "Home");
        }

        #endregion

        #region Добавление захоронения (неподтвержденного)

        [Authorize]
        [HttpGet]
        public ActionResult AddDeceased()
        {
            //SelectList items = new SelectList(deceasedDal.GetAllCategories(), "Id", "CategoryName");
            //ViewBag.Categories = items;
            ViewBag.Categories2 = deceasedDal.GetAllCategories();
            return View();
        }

        [HttpPost]//Принимаюший метод
        public ActionResult AddDeceased(string LastName, string FirstName, string Parcicle, DateTime? DateBirth, DateTime? DateDead, string Descript, HttpPostedFileBase upload, int[] selectedCateg)
        {
            //получаем файл
            if (upload != null)
            {
                fileName = System.IO.Path.GetFileName(upload.FileName);//получам путь файла               
                upload.SaveAs(Server.MapPath("/Content/Images/Photos/" + fileName));// сохраняем файл в папку Files в проекте
            }
            else
            {
                fileName = "notphoto.jpg";// Если файл отсутсвует, загрузить картинку, нет фото.
            }

            #region Конструктор

            Deceased man = new Deceased
            {
                FName = FirstName,
                LName = LastName,
                SName = Parcicle,
                DOB = DateBirth,
                DateDeath = DateDead,
                Description = Descript,
                Photo = "/Content/Images/Photos/" + fileName
            };

            #endregion


            deceasedDal.AddDeceased(man, selectedCateg);

            return RedirectToAction("Search");//Возврат на страницу
        }

        #endregion
    }
}