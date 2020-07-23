using DalilakProject.Database;
using DalilakProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace DalilakProject.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register()
        {
            var model = new AuthModel();
            model.user = new Database.User();
            return View(model);
        }
        public ActionResult SaveUser(AuthModel model)
        {
            using (var db = new DalilakDatabaseEntities1())
            {
                db.Users.Add(model.user);
                db.SaveChanges();
            }
            return View("Login");
        }

        public ActionResult FindUser(AuthModel model)
        {
            using (var db = new DalilakDatabaseEntities1())
            {
                var loged_user = db.Users.Where(m => m.Username == model.user.Username && m.Password == model.user.Password).FirstOrDefault();
                if (loged_user != null)
                {
                    //logged in
                    //goto suggeset
                    var identity = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Name, loged_user.Id.ToString())
            },
          "ApplicationCookie");

                    var ctx = Request.GetOwinContext();
                    var authManager = ctx.Authentication;

                    authManager.SignIn(identity);

                    return RedirectToAction("Index", "Suggeset");
                }
                else
                {
                    //incorrect in username or password
                    ViewBag.ErrorMessage = "Incorrect username or password";
                    return View("Login");
                }
            }
           
        }

        public ActionResult Login()
        {
            var model = new AuthModel();
            model.user = new Database.User();
            return View(model);
        }
    }
}