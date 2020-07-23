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
using DalilakProject.Models;

namespace DalilakProject.Controllers
{
    public class LocationsController : Controller
    {
        private DalilakDatabaseEntities1 db = new DalilakDatabaseEntities1();

        // GET: Locations
        public ActionResult Index()
        {
            var locations = db.Locations.Include(l => l.Category).Include(l => l.Area);
            return View(locations.ToList());
        }

        // GET: Locations/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        // GET: Locations/Create
        public ActionResult Create()
        {
            ViewBag.FK_Category = new SelectList(db.Categories, "Id", "CategoryName");
            ViewBag.FK_AreaId = new SelectList(db.Areas, "Id", "AreaName");
            return View();
        }

        // POST: Locations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LocationModel model)
        {
            string file_name = "";
            if (model.img != null && model.img.ContentLength > 0)
                try
                {
                    file_name = Guid.NewGuid() + Path.GetFileName(model.img.FileName);
                    string path = Path.Combine(Server.MapPath("~/images"),
                                              file_name);
                    model.img.SaveAs(path);

                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            model.location.Image = file_name;
            if (ModelState.IsValid)
            {
                db.Locations.Add(model.location);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FK_Category = new SelectList(db.Categories, "Id", "CategoryName", model.location.FK_Category);
            ViewBag.FK_AreaId = new SelectList(db.Areas, "Id", "AreaName", model.location.FK_AreaId);
            return View(model.location);
        }

        // GET: Locations/Edit/5
        public ActionResult Edit(long? id)
        {
            var model = new LocationModel();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            model.location = db.Locations.Find(id);
            if (model.location == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_Category = new SelectList(db.Categories, "Id", "CategoryName", model.location.FK_Category);
            ViewBag.FK_AreaId = new SelectList(db.Areas, "Id", "AreaName", model.location.FK_AreaId);
            return View(model);
        }

        // POST: Locations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LocationModel model)
        {
            string file_name = "";
            if (model.img != null && model.img.ContentLength > 0)
                try
                {
                    file_name = Guid.NewGuid() + Path.GetFileName(model.img.FileName);
                    string path = Path.Combine(Server.MapPath("~/images"),
                                              file_name);
                    model.img.SaveAs(path);

                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            if (!string.IsNullOrEmpty(file_name))
            {
                model.location.Image = file_name;
            }

            if (ModelState.IsValid)
            {
                db.Entry(model.location).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_Category = new SelectList(db.Categories, "Id", "CategoryName", model.location.FK_Category);
            ViewBag.FK_AreaId = new SelectList(db.Areas, "Id", "AreaName", model.location.FK_AreaId);
            return View(model.location);
        }

        // GET: Locations/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        // POST: Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Location location = db.Locations.Find(id);
            db.Locations.Remove(location);
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
