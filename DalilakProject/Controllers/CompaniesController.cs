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
    public class CompaniesController : Controller
    {
        private DalilakDatabaseEntities1 db = new DalilakDatabaseEntities1();

        // GET: Companies
        public ActionResult Index()
        {
            var companies = db.Companies.Include(c => c.Area).Include(c => c.Category);
            return View(companies.ToList());
        }

        // GET: Companies/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // GET: Companies/Create
        public ActionResult Create()
        {
            ViewBag.FK_AreaId = new SelectList(db.Areas, "Id", "AreaName");
            ViewBag.FK_Category = new SelectList(db.Categories, "Id", "CategoryName");
            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CompanyName,Description,Latitude,Longitude,Email,Address,FK_Category,Phone,Image,FK_AreaId")] Company company,HttpPostedFileBase img)
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
                company.Image = file_name;

                db.Companies.Add(company);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FK_AreaId = new SelectList(db.Areas, "Id", "AreaName", company.FK_AreaId);
            ViewBag.FK_Category = new SelectList(db.Categories, "Id", "CategoryName", company.FK_Category);
            return View(company);
        }

        // GET: Companies/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_AreaId = new SelectList(db.Areas, "Id", "AreaName", company.FK_AreaId);
            ViewBag.FK_Category = new SelectList(db.Categories, "Id", "CategoryName", company.FK_Category);
            return View(company);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CompanyName,Description,Latitude,Longitude,Email,Address,FK_Category,Phone,Image,FK_AreaId")] Company company,HttpPostedFileBase img)
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
                company.Image = file_name;
                db.Entry(company).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_AreaId = new SelectList(db.Areas, "Id", "AreaName", company.FK_AreaId);
            ViewBag.FK_Category = new SelectList(db.Categories, "Id", "CategoryName", company.FK_Category);
            return View(company);
        }

        // GET: Companies/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Company company = db.Companies.Find(id);
            db.Companies.Remove(company);
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
