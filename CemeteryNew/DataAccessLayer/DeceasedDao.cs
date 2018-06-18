using CemeteryNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace CemeteryNew.DataAccessLayer
{
    public class DeceasedDao
    {
        DataContext DB = new DataContext();

        /// <summary>
        /// Получает список всех захоронений
        /// </summary>
        /// <param name="ShowUnconfirmed">true, если хотите получить список всех захоронений</param>
        /// <returns></returns>
        public IQueryable<Deceased> GetAllDeceased(bool ShowUnconfirmed = false)
        {
            IQueryable<Deceased> deceaseds = null;
            if (ShowUnconfirmed)
                deceaseds = DB.Deceaseds.Include(s => s.Categories).Include(s => s.BurialPlace).Where(c => c.Confirmed == true);
            else
                deceaseds = DB.Deceaseds.Include(s => s.Categories).Include(s => s.BurialPlace);

            return deceaseds;
        }

        /// <summary>
        /// Получает список неподтвержденных захоронений
        /// </summary>
        /// <returns></returns>
        public List<Deceased> GetAllUnconfirmed()
        {
            List<Deceased> deceaseds = null;
            deceaseds = DB.Deceaseds.Include(s => s.Categories).Include(s => s.BurialPlace).Where(c => c.Confirmed == false).ToList();
            return deceaseds;
        }

        /// <summary>
        /// Добавляет захоронение в список неподтвержденных
        /// </summary>
        /// <param name="Add"></param>
        public void AddDeceased(Deceased Add)
        {
            DB.Deceaseds.Add(Add);
            DB.SaveChanges();
        }

        /// <summary>
        /// Возвращает первую найденную категорию с заданным именем
        /// </summary>
        /// <param name="CategoryName"></param>
        /// <returns></returns>
        public Category GetCategory(string CategoryName)
        {
            Category categ = DB.Categories.FirstOrDefault(d => d.CategoryName == CategoryName);
            return categ;
        }

        /// <summary>
        /// Возвращает найденный в базе объект захоронения, либо null
        /// </summary>
        /// <param name="NArea">Номер участка</param>
        /// <param name="NBurial">Номер могилы</param>
        /// <returns></returns>
        public BurialPlace GetBurial(int NArea, int NBurial)
        {
            BurialPlace burplace = DB.BurialPlaces.FirstOrDefault(x => x.NArea == NArea && x.NBurial == NBurial);
            return burplace;
        }
    }
}