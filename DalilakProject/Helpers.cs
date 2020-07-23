using DalilakProject.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DalilakProject
{
    public class Helpers
    {
        public static List<Database.Category> LoadCategories()
        {
            using (var db = new DalilakDatabaseEntities1())
            {
                return db.Categories.OrderBy(m => m.MenuOrder).ToList();
            }
        }

        public static List<Database.Area> LoadAreas()
        {
            using (var db = new DalilakDatabaseEntities1())
            {
                return db.Areas.ToList();
            }
        }

    }
}