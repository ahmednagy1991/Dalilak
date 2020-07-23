using DalilakProject.Database;
using DalilakProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DalilakProject.Controllers
{
    [Authorize]
    public class SuggesetController : Controller
    {
        // GET: Suggeset
        public ActionResult Index()
        {
            var model = new SuggesetModel();
            model.suggeset = new Database.Suggeste();
            return View(model);
        }

        public ActionResult SaveSuggeset(SuggesetModel model)
        {
            using(var db=new DalilakDatabaseEntities1())
            {
                model.suggeset.FK_UserID = long.Parse(User.Identity.Name);
                db.Suggestes.Add(model.suggeset);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}