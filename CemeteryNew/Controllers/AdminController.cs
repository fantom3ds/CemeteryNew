using CemeteryNew.DataAccessLayer;
using CemeteryNew.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CemeteryNew.Controllers
{
    [Authorize(Roles = "admin")]
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
            ViewBag.Categories = deceasedDal.GetAllCategories();
            Deceased deceased = deceasedDal.GetDeceased(Id);
            return View(deceased);
        }

        [HttpPost]
        public ActionResult EditBurial(Deceased edited, HttpPostedFileBase upload)
        {
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

            if (edited.Photo == "")
                edited.Photo = fileName;

            deceasedDal.UpdateDeceased(edited);
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