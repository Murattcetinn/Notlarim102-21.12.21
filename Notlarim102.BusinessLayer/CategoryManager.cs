using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notlarim102.DataAccessLayer.EntityFramework;
using Notlarim102.Entity;

namespace Notlarim102.BusinessLayer
{
     public class CategoryManager
     {
         private Repository<Category> rcat = new Repository<Category>();

         public List<Category> GetCategories()
         {
             return rcat.List();
         }
        public Category GetCategoriesById(int id)
         {
             return rcat.Find(s=>s.Id==id);
         }

     }
}
