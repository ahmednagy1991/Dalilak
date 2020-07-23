using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DalilakProject.Database;

namespace DalilakProject.Controllers
{
    public class FootballCourtsController : Controller
    {
        private DalilakDatabaseEntities1 db = new DalilakDatabaseEntities1();

        // GET: FootballCourts
        public ActionResult Index()
        {
            var footballCourts = db.FootballCourts.Include(f => f.Area).Include(f => f.Category);
            return View(footballCourts.ToList());
        }

        // GET: FootballCourts/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FootballCourt footballCourt = db.FootballCourts.Find(id);
            if (footballCourt == null)
            {
                return HttpNotFound();
            }
            return View(footballCourt);
        }

        // GET: FootballCourts/Create
        public ActionResult Create()
        {
            ViewBag.FK_AreaId = new SelectList(db.Areas, "Id", "AreaName");
            ViewBag.FK_Category = new SelectList(db.Categories, "Id", "CategoryName");
            return View();
        }

        // POST: FootballCourts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FootballCourtName,Description,Latitude,Longitude,Email,Address,FK_Category,Phone,Image,FK_AreaId,Website")] FootballCourt footballCourt,HttpPostedFileBase img)
        {
            if (ModelState.IsValid)
            {
                string file_name = "";
                if (img != null && img.ContentLength > 0)
                    try
                    {
                        file_name = Guid.NewGuid() + Path.GetFileName(img.FileName);
                        string path = Path.Combine(Server.MapPath("~/images"),
                                                  file_name);
                        img.SaveAs(path);

                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "ERROR:" + ex.Message.ToString();
                    }
                footballCourt.Image = file_name;

                db.FootballCourts.Add(footballCourt);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FK_AreaId = new SelectList(db.Areas, "Id", "AreaName", footballCourt.FK_AreaId);
            ViewBag.FK_Category = new SelectList(db.Categories, "Id", "CategoryName", footballCourt.FK_Category);
            return View(footballCourt);
        }

        // GET: FootballCourts/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FootballCourt footballCourt = db.FootballCourts.Find(id);
            if (footballCourt == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_AreaId = new SelectList(db.Areas, "Id", "AreaName", footballCourt.FK_AreaId);
            ViewBag.FK_Category = new SelectList(db.Categories, "Id", "CategoryName", footballCourt.FK_Category);
            return View(footballCourt);
        }

        // POST: FootballCourts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FootballCourtName,Description,Latitude,Longitude,Email,Address,FK_Category,Phone,Image,FK_AreaId,Website")] FootballCourt footballCourt,HttpPostedFileBase img)
        {
            if (ModelState.IsValid)
            {
                string file_name = "";
                if (img != null && img.ContentLength > 0)
                    try
                    {
                        file_name = Guid.NewGuid() + Path.GetFileName(img.FileName);
                        string path = Path.Combine(Server.MapPath("~/images"),
                                                  file_name);
                        img.SaveAs(path);

                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "ERROR:" + ex.Message.ToString();
                    }
                footballCourt.Image = file_name;

                db.Entry(footballCourt).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_AreaId = new SelectList(db.Areas, "Id", "AreaName", footballCourt.FK_AreaId);
            ViewBag.FK_Category = new SelectList(db.Categories, "Id", "CategoryName", footballCourt.FK_Category);
            return View(footballCourt);
        }

        // GET: FootballCourts/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FootballCourt footballCourt = db.FootballCourts.Find(id);
            if (footballCourt == null)
            {
                return HttpNotFound();
            }
            return View(footballCourt);
        }

        // POST: FootballCourts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            FootballCourt footballCourt = db.FootballCourts.Find(id);
            db.FootballCourts.Remove(footballCourt);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
