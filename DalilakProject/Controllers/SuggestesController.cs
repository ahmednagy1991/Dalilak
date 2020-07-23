using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DalilakProject.Database;

namespace DalilakProject.Controllers
{
    public class SuggestesController : Controller
    {
        private DalilakDatabaseEntities1 db = new DalilakDatabaseEntities1();

        // GET: Suggestes
        public ActionResult Index()
        {
            var suggestes = db.Suggestes.Include(s => s.User);
            return View(suggestes.ToList());
        }

        // GET: Suggestes/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Suggeste suggeste = db.Suggestes.Find(id);
            if (suggeste == null)
            {
                return HttpNotFound();
            }
            return View(suggeste);
        }

        // GET: Suggestes/Create
        public ActionResult Create()
        {
            ViewBag.FK_UserID = new SelectList(db.Users, "Id", "Username");
            return View();
        }

        // POST: Suggestes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Suggeset,FK_UserID")] Suggeste suggeste)
        {
            if (ModelState.IsValid)
            {
                db.Suggestes.Add(suggeste);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FK_UserID = new SelectList(db.Users, "Id", "Username", suggeste.FK_UserID);
            return View(suggeste);
        }

        // GET: Suggestes/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Suggeste suggeste = db.Suggestes.Find(id);
            if (suggeste == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_UserID = new SelectList(db.Users, "Id", "Username", suggeste.FK_UserID);
            return View(suggeste);
        }

        // POST: Suggestes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Suggeset,FK_UserID")] Suggeste suggeste)
        {
            if (ModelState.IsValid)
            {
                db.Entry(suggeste).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_UserID = new SelectList(db.Users, "Id", "Username", suggeste.FK_UserID);
            return View(suggeste);
        }

        // GET: Suggestes/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Suggeste suggeste = db.Suggestes.Find(id);
            if (suggeste == null)
            {
                return HttpNotFound();
            }
            return View(suggeste);
        }

        // POST: Suggestes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Suggeste suggeste = db.Suggestes.Find(id);
            db.Suggestes.Remove(suggeste);
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
