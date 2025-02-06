﻿using System;
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
    public class Job_DetailController : Controller
    {
        private JOBPORTALDBEntities db = new JOBPORTALDBEntities();

        // GET: Job_Detail
        public ActionResult Index()
        {
            var job_Details = db.Job_Details.Include(j => j.Application_Settings).Include(j => j.Company_Information);
            return View(job_Details.ToList());
        }

        // GET: Job_Detail/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job_Detail job_Detail = db.Job_Details.Find(id);
            if (job_Detail == null)
            {
                return HttpNotFound();
            }
            return View(job_Detail);
        }

        // GET: Job_Detail/Create
        public ActionResult Create()
        {
            ViewBag.JobID = new SelectList(db.Application_Settings, "ApplicationID", "ApplicationEmail");
            ViewBag.JobID = new SelectList(db.Company_Information, "CompanyID", "CompanyName");
            return View();
        }

        // POST: Job_Detail/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JobID,CompanyID,JobTitle,JobCategory,Location,Worktype,SalaryMin,SalaryMax,ExperienceLevel,Jobdescripton,Reqirements,Benefits")] Job_Detail job_Detail)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Job_Details.Add(job_Detail);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Log the error message
                    ModelState.AddModelError("", "Error saving changes: " + ex.Message);
                    // Optionally log the exception details
                    // Log.Error(ex); // If using a logger like NLog, Serilog, etc.
                }
            }
            else
            {
                // If ModelState is invalid, log the issues
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                ModelState.AddModelError("", string.Join(", ", errors));
            }

            // If we reach here, there's an issue, so we return the form with the errors
            ViewBag.JobID = new SelectList(db.Application_Settings, "ApplicationID", "ApplicationEmail", job_Detail.JobID);
            ViewBag.CompanyID = new SelectList(db.Company_Information, "CompanyID", "CompanyName", job_Detail.CompanyID);
            return View(job_Detail);
        }




        // GET: Job_Detail/Edit/5
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(Job_Detail job)
        {
            if (ModelState.IsValid)
            {
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(job);
        }


        // POST: Job_Detail/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [ValidateAntiForgeryToken]
        [HttpPost] 
        public ActionResult edit(Job_Detail job)
        {
            if (ModelState.IsValid)
            {
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(job);
        }


        // GET: Job_Detail/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job_Detail job_Detail = db.Job_Details.Find(id);
            if (job_Detail == null)
            {
                return HttpNotFound();
            }
            return View(job_Detail);
        }

        // POST: Job_Detail/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var job = db.Job_Details.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }

            db.Job_Details.Remove(job);
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
