using CemeteryNew.DataAccessLayer;
using CemeteryNew.Models;
using CemeteryNew.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CemeteryNew.Controllers
{
    [Authorize(Roles ="admin")]
    public class AdminController : Controller
    {
        private UserDal userDal;
        private DeceasedDal deceasedDal;

        public AdminController()
        {
            userDal = new UserDal();
            deceasedDal = new DeceasedDal();
        }

        public ActionResult UnknownBurials()
        {
            List<Deceased> deceaseds;
            using (DataContext DB = new DataContext())
            {
                deceaseds = new List<Deceased>(DB.Deceaseds.Where(c => c.Confirmed == false).Include(c => c.Categories).Include(a => a.BurialPlace));
            }
            return View(deceaseds);
        }

        #region Редактирование

        public ActionResult EditBurial(int Id)
        {
            Deceased deceased = null;
            DeceasedModel deceased2 = null;
            using (DataContext DB = new DataContext())
            {
                deceased = DB.Deceaseds.Include(c => c.BurialPlace).Include(x => x.Categories).FirstOrDefault(b => b.Id == Id);
                if (deceased == null)
                    return HttpNotFound("Захоронение не найдено, повторите попытку позже");

                deceased2 = new DeceasedModel(deceased);
            }
            return View(deceased2);
        }

        [HttpPost]
        public ActionResult EditBurial(DeceasedModel edited, HttpPostedFileBase upload)
        {
            Deceased deceased = null;
            using (DataContext DB = new DataContext())
            {
                deceased = DB.Deceaseds.Include(c => c.Categories).Include(d => d.BurialPlace).FirstOrDefault(i => i.Id == edited.Id);
                if (deceased == null)
                    return HttpNotFound("Захоронение не найдено, повторите попытку позже");

                string fileName = "";
                if (upload != null)
                {
                    fileName = System.IO.Path.GetFileName(upload.FileName);//получам путь файла               
                    upload.SaveAs(Server.MapPath("/Content/Images/Photos/" + fileName));// сохраняем файл в папку Files в проекте
                }
                else
                {
                    fileName = "notphoto.jpg";// Если файл отсутсвует, загрузить картинку, нет фото.
                }

                //Присваиваем могиле ее номера
                //if (edited.NBur == null)
                //    edited.NBur = 0;
                //if (edited.NArea == null)
                //    edited.NArea = 0;

                //Ищем могилу в базе,если существует - пусть будет она. Если нет - копаем
                //var burplace = DB.BurialPlaces.FirstOrDefault(x => x.NArea == edited.NArea && x.NBurial == edited.NBur);
                //if (burplace == null)
                //    burplace = new BurialPlace { NArea = (int)edited.NArea, NBurial = (int)edited.NBur };


                deceased.FName = edited.FName;
                deceased.LName = edited.LName;
                deceased.SName = edited.SName;
                deceased.DOB = edited.DOB;
                deceased.DateDeath = edited.DateDeath;
                if (edited.Photo == "")
                    deceased.Photo = fileName;
                deceased.Description = edited.Description;
                //if (deceased.BurialPlace == null)
                //{
                //    deceased.BurialPlace = burplace;
                //}
                //else
                //{
                //    if (edited.NArea != 0 && edited.NBur!=0)
                //    {

                //    }
                //}
                DB.SaveChanges();
            }
            return RedirectToAction("UnknownBurials");
        }

        #endregion

        #region Подтверждение/Снятие подтверждения

        public ActionResult ConfirmBurial(int Id)
        {
            Deceased deceased = deceasedDal.GetDeceased(Id);
            if (deceased == null)
                return HttpNotFound();
            return View(deceased);
        }

        [HttpPost]
        public ActionResult ConfirmBurial(Deceased model)
        {
            deceasedDal.ConfirmBurial(model.Id);
            return RedirectToAction("UnknownBurials");
        }

        [HttpGet]
        public ActionResult UnconfirmBurial(int Id)
        {
            deceasedDal.UnConfirmBurial(Id);
            return RedirectToAction("Search", "Home");
        }

        #endregion

        #region Удаление

        public ActionResult DeleteBurial(int Id)
        {
            Deceased choice = deceasedDal.GetDeceased(Id);
            if (choice == null)
                return HttpNotFound("Захоронение не найдено, повторите попытку позже");
            return View(choice);
        }

        [HttpPost]
        [ActionName("DeleteBurial")]
        public ActionResult ConfirmDeleteBurial(int Id)
        {
            Deceased choice = null;
            using (DataContext DB = new DataContext())
            {
                choice = DB.Deceaseds.Find(Id);
                if (choice == null)
                    return HttpNotFound("Захоронение не найдено, повторите попытку позже");
                DB.Deceaseds.Remove(choice);
                DB.SaveChanges();
            }
            return RedirectToAction("UnknownBurials");
        }

        #endregion
    }
}