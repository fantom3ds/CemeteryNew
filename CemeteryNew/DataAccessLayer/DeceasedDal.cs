using CemeteryNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace CemeteryNew.DataAccessLayer
{
    public class DeceasedDal
    {
        #region Получение списка захоронений

        /// <summary>
        /// Возвращает захоронение по его Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Deceased GetDeceased(int Id)
        {
            Deceased deceased = null;
            using (DataContext Db = new DataContext())
            {
                deceased = Db.Deceaseds.Include(b => b.BurialPlace).Include(c => c.Categories).FirstOrDefault(i => i.Id == Id);
            }
            return deceased;
        }

        /// <summary>
        /// Получает список всех захоронений
        /// </summary>
        /// <param name="ShowUnconfirmed">true, если хотите получить список всех захоронений</param>
        /// <returns></returns>
        public IQueryable<Deceased> GetAllDeceased(bool ShowUnconfirmed = false)
        {
            DataContext Db = new DataContext();
            IQueryable<Deceased> deceaseds = null;
            if (ShowUnconfirmed)
                deceaseds = Db.Deceaseds.Include(s => s.Categories).Include(s => s.BurialPlace).Where(c => c.Confirmed == true);
            else
                deceaseds = Db.Deceaseds.Include(s => s.Categories).Include(s => s.BurialPlace);
            return deceaseds;
        }

        /// <summary>
        /// Получает список неподтвержденных захоронений
        /// </summary>
        /// <returns></returns>
        public List<Deceased> GetAllUnconfirmed()
        {
            List<Deceased> deceaseds = null;
            using (DataContext DB = new DataContext())
            {
                deceaseds = DB.Deceaseds.Include(s => s.Categories).Include(s => s.BurialPlace).Where(c => c.Confirmed == false).ToList();
            }
            return deceaseds;
        }

        #endregion

        /// <summary>
        /// Добавляет захоронение в список неподтвержденных
        /// </summary>
        /// <param name="Add"></param>
        public void AddDeceased(Deceased Add, int CategoryId = 0)
        {
            using (DataContext Context = new DataContext())
            {
                if (CategoryId == 0)
                {
                    Context.Deceaseds.Add(Add);
                }
                else
                {
                    Add.Categories = new List<Category>();
                    Add.Categories.Add(Context.Categories.Find(CategoryId));
                    Context.Deceaseds.Add(Add);
                }
                Context.SaveChanges();
            }
        }

        #region Возвращаение категорий

        /// <summary>
        /// Возвращает первую найденную категорию с заданным именем
        /// </summary>
        /// <param name="CategoryName"></param>
        /// <returns></returns>
        public Category GetCategory(string CategoryName)
        {
            Category categ;
            using (DataContext DB = new DataContext())
            {
                categ = DB.Categories.FirstOrDefault(d => d.CategoryName == CategoryName);
            }
            return categ;
        }
        /// <summary>
        /// Возвращает категорию по ее Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Category GetCategory(int Id)
        {
            Category categ;
            using (DataContext DB = new DataContext())
            {
                categ = DB.Categories.Find(Id);
            }
            return categ;
        }

        /// <summary>
        /// Возвращает коллекцию всех категорий
        /// </summary>
        /// <returns></returns>
        public List<Category> GetAllCategories()
        {
            List<Category> categories = null;
            using (DataContext DB = new DataContext())
            {
                categories = DB.Categories.ToList();
            }
            return categories;
        }

        #endregion


        /// <summary>
        /// Возвращает найденный в базе объект захоронения, либо null
        /// </summary>
        /// <param name="NArea">Номер участка</param>
        /// <param name="NBurial">Номер могилы</param>
        /// <returns></returns>
        public BurialPlace GetBurial(int NArea, int NBurial)
        {
            BurialPlace burplace = null;
            using (DataContext DB = new DataContext())
            {
                burplace = DB.BurialPlaces.FirstOrDefault(x => x.NArea == NArea && x.NBurial == NBurial);
            }
            return burplace;
        }


        #region Подтвердить/Снять подтверждение

        public void ConfirmBurial(int Id)
        {
            using (DataContext DB = new DataContext())
            {
                Deceased deceased = DB.Deceaseds.Find(Id);
                if (deceased != null)
                {
                    deceased.Confirmed = true;
                    DB.SaveChanges();
                }
                else
                    throw new KeyNotFoundException
                        ("Захоронение не найдено, возможно устарели данные сессии");
            }
        }

        public void UnConfirmBurial(int Id)
        {
            using (DataContext DB = new DataContext())
            {
                Deceased deceased = DB.Deceaseds.Find(Id);
                if (deceased != null)
                {
                    deceased.Confirmed = false;
                    DB.SaveChanges();
                }
                else
                    throw new KeyNotFoundException
                        ("Захоронение не найдено, возможно устарели данные сессии");
            }
        }

        #endregion
    }
}