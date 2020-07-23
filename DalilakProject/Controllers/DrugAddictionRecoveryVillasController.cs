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
    public class DrugAddictionRecoveryVillasController : Controller
    {
        private DalilakDatabaseEntities1 db = new DalilakDatabaseEntities1();

        // GET: DrugAddictionRecoveryVillas
        public ActionResult Index()
        {
            var drugAddictionRecoveryVillas = db.DrugAddictionRecoveryVillas.Include(d => d.Area).Include(d => d.Category);
            return View(drugAddictionRecoveryVillas.ToList());
        }

        // GET: DrugAddictionRecoveryVillas/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DrugAddictionRecoveryVilla drugAddictionRecoveryVilla = db.DrugAddictionRecoveryVillas.Find(id);
            if (drugAddictionRecoveryVilla == null)
            {
                return HttpNotFound();
            }
            return View(drugAddictionRecoveryVilla);
        }

        // GET: DrugAddictionRecoveryVillas/Create
        public ActionResult Create()
        {
            ViewBag.FK_AreaId = new SelectList(db.Areas, "Id", "AreaName");
            ViewBag.FK_Category = new SelectList(db.Categories, "Id", "CategoryName");
            return View();
        }

        // POST: DrugAddictionRecoveryVillas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DrugAddictionRecoveryVillaName,Description,Latitude,Longitude,Email,Address,FK_Category,Phone,Image,FK_AreaId,Website")] DrugAddictionRecoveryVilla drugAddictionRecoveryVilla,HttpPostedFileBase img)
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
                drugAddictionRecoveryVilla.Image = file_name;

                db.DrugAddictionRecoveryVillas.Add(drugAddictionRecoveryVilla);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FK_AreaId = new SelectList(db.Areas, "Id", "AreaName", drugAddictionRecoveryVilla.FK_AreaId);
            ViewBag.FK_Category = new SelectList(db.Categories, "Id", "CategoryName", drugAddictionRecoveryVilla.FK_Category);
            return View(drugAddictionRecoveryVilla);
        }

        // GET: DrugAddictionRecoveryVillas/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DrugAddictionRecoveryVilla drugAddictionRecoveryVilla = db.DrugAddictionRecoveryVillas.Find(id);
            if (drugAddictionRecoveryVilla == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_AreaId = new SelectList(db.Areas, "Id", "AreaName", drugAddictionRecoveryVilla.FK_AreaId);
            ViewBag.FK_Category = new SelectList(db.Categories, "Id", "CategoryName", drugAddictionRecoveryVilla.FK_Category);
            return View(drugAddictionRecoveryVilla);
        }

        // POST: DrugAddictionRecoveryVillas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DrugAddictionRecoveryVillaName,Description,Latitude,Longitude,Email,Address,FK_Category,Phone,Image,FK_AreaId,Website")] DrugAddictionRecoveryVilla drugAddictionRecoveryVilla,HttpPostedFileBase img)
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
                drugAddictionRecoveryVilla.Image = file_name;


                db.Entry(drugAddictionRecoveryVilla).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_AreaId = new SelectList(db.Areas, "Id", "AreaName", drugAddictionRecoveryVilla.FK_AreaId);
            ViewBag.FK_Category = new SelectList(db.Categories, "Id", "CategoryName", drugAddictionRecoveryVilla.FK_Category);
            return View(drugAddictionRecoveryVilla);
        }

        // GET: DrugAddictionRecoveryVillas/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DrugAddictionRecoveryVilla drugAddictionRecoveryVilla = db.DrugAddictionRecoveryVillas.Find(id);
            if (drugAddictionRecoveryVilla == null)
            {
                return HttpNotFound();
            }
            return View(drugAddictionRecoveryVilla);
        }

        // POST: DrugAddictionRecoveryVillas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            DrugAddictionRecoveryVilla drugAddictionRecoveryVilla = db.DrugAddictionRecoveryVillas.Find(id);
            db.DrugAddictionRecoveryVillas.Remove(drugAddictionRecoveryVilla);
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
