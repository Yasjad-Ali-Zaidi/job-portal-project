using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataBaseLayer;

namespace JobPortal.Controllers
{
    public class Application_SettingController : Controller
    {
        private JOBPORTALDBEntities db = new JOBPORTALDBEntities();

        // GET: Application_Setting
        public ActionResult Index()
        {
            var application_Settings = db.Application_Settings.Include(a => a.Job_Details);
            return View(application_Settings.ToList());
        }

        // GET: Application_Setting/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application_Setting application_Setting = db.Application_Settings.Find(id);
            if (application_Setting == null)
            {
                return HttpNotFound();
            }
            return View(application_Setting);
        }

        // GET: Application_Setting/Create
        public ActionResult Create()
        {
            ViewBag.ApplicationID = new SelectList(db.Job_Details, "JobID", "JobTitle");
            return View();
        }

        // POST: Application_Setting/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JobID,ApplicationDeadline,ApplicationEmail,ReceiveByEmail")] Application_Setting application_Setting)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Application_Settings.Add(application_Setting);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error saving data: " + ex.InnerException?.Message ?? ex.Message);
                }
            }

            ViewBag.JobID = new SelectList(db.Job_Details, "JobID", "JobTitle", application_Setting.JobID);
            return View(application_Setting);
        }



        // GET: Application_Setting/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application_Setting application_Setting = db.Application_Settings.Find(id);
            if (application_Setting == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationID = new SelectList(db.Job_Details, "JobID", "JobTitle", application_Setting.ApplicationID);
            return View(application_Setting);
        }

        // POST: Application_Setting/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ApplicationID,JobID,ApplicationDeadline,ApplicationEmail,ReceiveByEmail")] Application_Setting application_Setting)
        {
            if (ModelState.IsValid)
            {
                db.Entry(application_Setting).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationID = new SelectList(db.Job_Details, "JobID", "JobTitle", application_Setting.ApplicationID);
            return View(application_Setting);
        }

        // GET: Application_Setting/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application_Setting application_Setting = db.Application_Settings.Find(id);
            if (application_Setting == null)
            {
                return HttpNotFound();
            }
            return View(application_Setting);
        }

        // POST: Application_Setting/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Application_Setting application_Setting = db.Application_Settings.Find(id);
                if (application_Setting == null)
                {
                    return HttpNotFound(); // Return 404 if not found
                }

                db.Application_Settings.Remove(application_Setting);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error deleting record: " + ex.Message);
                return RedirectToAction("Delete", new { id });
            }

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
