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
    public class EmergencyPhoneNumbersController : Controller
    {
        private DalilakDatabaseEntities1 db = new DalilakDatabaseEntities1();

        // GET: EmergencyPhoneNumbers
        public ActionResult Index()
        {
            var emergencyPhoneNumbers = db.EmergencyPhoneNumbers.Include(e => e.Area).Include(e => e.Category);
            return View(emergencyPhoneNumbers.ToList());
        }

        // GET: EmergencyPhoneNumbers/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmergencyPhoneNumber emergencyPhoneNumber = db.EmergencyPhoneNumbers.Find(id);
            if (emergencyPhoneNumber == null)
            {
                return HttpNotFound();
            }
            return View(emergencyPhoneNumber);
        }

        // GET: EmergencyPhoneNumbers/Create
        public ActionResult Create()
        {
            ViewBag.FK_AreaId = new SelectList(db.Areas, "Id", "AreaName");
            ViewBag.FK_Category = new SelectList(db.Categories, "Id", "CategoryName");
            return View();
        }

        // POST: EmergencyPhoneNumbers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EmergencyPhoneNumberName,Description,Latitude,Longitude,Email,Address,FK_Category,Phone,Image,FK_AreaId,Website")] EmergencyPhoneNumber emergencyPhoneNumber,HttpPostedFileBase img)
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
                emergencyPhoneNumber.Image = file_name;

                db.EmergencyPhoneNumbers.Add(emergencyPhoneNumber);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FK_AreaId = new SelectList(db.Areas, "Id", "AreaName", emergencyPhoneNumber.FK_AreaId);
            ViewBag.FK_Category = new SelectList(db.Categories, "Id", "CategoryName", emergencyPhoneNumber.FK_Category);
            return View(emergencyPhoneNumber);
        }

        // GET: EmergencyPhoneNumbers/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmergencyPhoneNumber emergencyPhoneNumber = db.EmergencyPhoneNumbers.Find(id);
            if (emergencyPhoneNumber == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_AreaId = new SelectList(db.Areas, "Id", "AreaName", emergencyPhoneNumber.FK_AreaId);
            ViewBag.FK_Category = new SelectList(db.Categories, "Id", "CategoryName", emergencyPhoneNumber.FK_Category);
            return View(emergencyPhoneNumber);
        }

        // POST: EmergencyPhoneNumbers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EmergencyPhoneNumberName,Description,Latitude,Longitude,Email,Address,FK_Category,Phone,Image,FK_AreaId,Website")] EmergencyPhoneNumber emergencyPhoneNumber,HttpPostedFileBase img)
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
                emergencyPhoneNumber.Image = file_name;

                db.Entry(emergencyPhoneNumber).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_AreaId = new SelectList(db.Areas, "Id", "AreaName", emergencyPhoneNumber.FK_AreaId);
            ViewBag.FK_Category = new SelectList(db.Categories, "Id", "CategoryName", emergencyPhoneNumber.FK_Category);
            return View(emergencyPhoneNumber);
        }

        // GET: EmergencyPhoneNumbers/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmergencyPhoneNumber emergencyPhoneNumber = db.EmergencyPhoneNumbers.Find(id);
            if (emergencyPhoneNumber == null)
            {
                return HttpNotFound();
            }
            return View(emergencyPhoneNumber);
        }

        // POST: EmergencyPhoneNumbers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            EmergencyPhoneNumber emergencyPhoneNumber = db.EmergencyPhoneNumbers.Find(id);
            db.EmergencyPhoneNumbers.Remove(emergencyPhoneNumber);
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
