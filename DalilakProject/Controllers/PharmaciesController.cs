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
    public class PharmaciesController : Controller
    {
        private DalilakDatabaseEntities1 db = new DalilakDatabaseEntities1();

        // GET: Pharmacies
        public ActionResult Index()
        {
            var pharmacies = db.Pharmacies.Include(p => p.Area).Include(p => p.Category);
            return View(pharmacies.ToList());
        }

        // GET: Pharmacies/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pharmacy pharmacy = db.Pharmacies.Find(id);
            if (pharmacy == null)
            {
                return HttpNotFound();
            }
            return View(pharmacy);
        }

        // GET: Pharmacies/Create
        public ActionResult Create()
        {
            ViewBag.FK_AreaId = new SelectList(db.Areas, "Id", "AreaName");
            ViewBag.FK_Category = new SelectList(db.Categories, "Id", "CategoryName");
            return View();
        }

        // POST: Pharmacies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PharmacyName,Description,Latitude,Longitude,Email,Address,FK_Category,Phone,Image,FK_AreaId,Website")] Pharmacy pharmacy,HttpPostedFileBase img)
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
                pharmacy.Image = file_name;

                db.Pharmacies.Add(pharmacy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FK_AreaId = new SelectList(db.Areas, "Id", "AreaName", pharmacy.FK_AreaId);
            ViewBag.FK_Category = new SelectList(db.Categories, "Id", "CategoryName", pharmacy.FK_Category);
            return View(pharmacy);
        }

        // GET: Pharmacies/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pharmacy pharmacy = db.Pharmacies.Find(id);
            if (pharmacy == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_AreaId = new SelectList(db.Areas, "Id", "AreaName", pharmacy.FK_AreaId);
            ViewBag.FK_Category = new SelectList(db.Categories, "Id", "CategoryName", pharmacy.FK_Category);
            return View(pharmacy);
        }

        // POST: Pharmacies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PharmacyName,Description,Latitude,Longitude,Email,Address,FK_Category,Phone,Image,FK_AreaId,Website")] Pharmacy pharmacy,HttpPostedFileBase img)
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
                pharmacy.Image = file_name;

                db.Entry(pharmacy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_AreaId = new SelectList(db.Areas, "Id", "AreaName", pharmacy.FK_AreaId);
            ViewBag.FK_Category = new SelectList(db.Categories, "Id", "CategoryName", pharmacy.FK_Category);
            return View(pharmacy);
        }

        // GET: Pharmacies/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pharmacy pharmacy = db.Pharmacies.Find(id);
            if (pharmacy == null)
            {
                return HttpNotFound();
            }
            return View(pharmacy);
        }

        // POST: Pharmacies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Pharmacy pharmacy = db.Pharmacies.Find(id);
            db.Pharmacies.Remove(pharmacy);
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
