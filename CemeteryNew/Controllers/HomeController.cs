using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using CemeteryNew.Models;
using CemeteryNew.Models.ViewModels;

namespace CemeteryNew.Controllers
{
    public class HomeController : Controller
    {
        string fileName; // переменная путь файла

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



        public ActionResult Search(List<Deceased> deceaseds)
        {
            SearchModel search = new SearchModel();
            using (DataContext DB = new DataContext())
            {
                search.Deceaseds = new List<Deceased>(DB.Deceaseds.Where(c => c.Confirmed == true).Include(c => c.Categories).Include(a => a.BurialPlace));
            }
            return View(search);
        }



        

        public ActionResult History()
        {
            return View();
        }//Страница истории 

        [Authorize]
        public ActionResult AddDeceased()
        {
            return View();
        }


        #region Добавление захоронения (неподтвержденного)


        [HttpPost]//Принимаюший метод
        public ActionResult AddDeceased(string LastName, string FirstName, string Parcicle, DateTime? DateBirth, DateTime? DateDead, int? NumUch, int? NumMog, string opis, string category, HttpPostedFileBase upload)
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

            using (DataContext DB = new DataContext())
            {
                if (NumMog == null)
                    NumMog = 0;
                if (NumUch == null)
                    NumUch = 0;
                //Ищем могилу в базе,если существует - пусть будет она. Если нет - копаем
                var burplace = DB.BurialPlaces.FirstOrDefault(x => x.NArea == NumUch && x.NBurial == NumMog);
                if (burplace == null)
                    burplace = new BurialPlace { NArea = (int)NumUch, NBurial = (int)NumMog };

                Category categ = DB.Categories.FirstOrDefault(d => d.CategoryName == category);

                Deceased man = new Deceased // Объявляем класс
                {
                    FName = FirstName,
                    LName = LastName,
                    SName = Parcicle,

                    DOB = DateBirth,
                    DateDeath = DateDead,
                    // Присобачил метод проверки: если место существует - то хороним
                    // там, или же создаем новое.
                    BurialPlace = burplace,
                    Description = opis,
                    //Надо присобачить выбор из раскрывающегося списка
                    Categories = new List<Category>(),
                    Photo = "/Content/Images/Photos/" + fileName
                };
                if (categ != null)
                    man.Categories.Add(categ);
                DB.Deceaseds.Add(man);//Запись в бд
                DB.SaveChanges();//Сохранение
            }
            return RedirectToAction("Search");//Возврат на страницу
        }

        #endregion


        ////метод поиска по бд
        //[HttpGet]
        //public ActionResult Find(string findtext)
        //{
        //    using (DataContext DB = new DataContext())
        //    {
        //        var men = from s in DB.Deceaseds//прописываем запрос
        //                  select s;
        //        if (!String.IsNullOrEmpty(findtext))
        //        {
        //            string s = "ss";


        //            men = men.Where(s => s.Search.Contains(findtext)).Include(c => c.Category).Include(a => a.BurialPlace);//Запрос поиска
        //        }
        //        return View(men.ToList());// вывод содержимого
        //    }
        //}


        #region MyRegion1

        //[HttpPost]//Принимаюший метод
        ////обработка данных страницы
        //public ActionResult AddDeceased(Deceased model, HttpPostedFileBase upload)
        //{
        //    //получаем файл
        //    if (upload != null)
        //    {
        //        fileName = System.IO.Path.GetFileName(upload.FileName);//получам путь файла               
        //        upload.SaveAs(Server.MapPath("/Content/Images/Photos/" + fileName));// сохраняем файл в папку Files в проекте
        //    }
        //    else
        //    {
        //        fileName = "notphoto.jpg";// Если файл отсутсвует, загрузить картинку, нет фото.
        //    }

        //    using (DataContext DB = new DataContext())
        //    {
        //        //Ищем могилу в базе,если существует - пусть будет она. Если нет - копаем
        //        var burplace = DB.BurialPlaces.FirstOrDefault(x => x.NArea == NumUch && x.NBurial == NumMog);
        //        if (burplace == null)
        //            burplace = new BurialPlace { NArea = NumUch, NBurial = NumMog };

        //        Category categ = DB.Categories.FirstOrDefault(d => d.CategoryName == category);


        //        Deceased man;
        //        DateTime Birth;
        //        DateTime Dead;
        //        if (DateTime.TryParse(DateBith, out Birth) && DateTime.TryParse(DateDead, out Dead))
        //        {

        //        }

        //        man = new Deceased // Объявляем класс
        //        {
        //            FName = FirstName,
        //            LName = LastName,
        //            SName = Parcicle,

        //            DOB = Birth,
        //            //DateDeath = Dead,
        //            // Присобачил метод проверки: если место существует - то хороним
        //            // там, или же создаем новое.
        //            BurialPlace = burplace,
        //            Description = opis,
        //            //Надо присобачить выбор из раскрывающегося списка
        //            Category = categ,
        //            Photo = "/Content/Images/Photos/" + fileName
        //        };
        //        DB.Deceaseds.Add(man);//Запись в бд
        //        DB.SaveChanges();//Сохранение
        //    }
        //    return RedirectToAction("Search");//Возврат на страницу
        //}

        #endregion
    }
}