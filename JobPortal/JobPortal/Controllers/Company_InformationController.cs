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
    public class Company_InformationController : Controller
    {
        private JOBPORTALDBEntities db = new JOBPORTALDBEntities();

        // GET: Company_Information
        public ActionResult Index()
        {
            var company_Information = db.Company_Information.Include(c => c.Job_Details);
            return View(company_Information.ToList());
        }

        // GET: Company_Information/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company_Information company_Information = db.Company_Information.Find(id);
            if (company_Information == null)
            {
                return HttpNotFound();
            }
            return View(company_Information);
        }

        // GET: Company_Information/Create
        public ActionResult Create()
        {
            ViewBag.CompanyID = new SelectList(db.Job_Details, "JobID", "JobTitle");
            return View();
        }

        // POST: Company_Information/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CompanyID,CompanyName,CompanyWebsite,CompanyLogo")] Company_Information company_Information)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Company_Information.Add(company_Information);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error saving data: " + ex.Message);
                }
            }

            ViewBag.CompanyID = new SelectList(db.Job_Details, "JobID", "JobTitle", company_Information.CompanyID);
            return View(company_Information);
        }


        // GET: Company_Information/Edit/5
        public ActionResult Edit(int id)
        {
            var company = db.Company_Information.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Company_Information/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Company_Information company)
        {
            if (company == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var existingCompany = db.Company_Information.Find(company.CompanyID);
            if (existingCompany == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                // Updating only specific fields
                existingCompany.CompanyName = company.CompanyName;
                existingCompany.CompanyWebsite = company.CompanyWebsite;
                existingCompany.CompanyLogo = company.CompanyLogo;

                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                {
                    foreach (var validationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            ModelState.AddModelError(validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }
                }
            }

            return View(company);
        }




        // GET: Company_Information/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Company_Information company = db.Company_Information.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }

            return View(company);
        }

        // POST: Company_Information/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Company_Information company = db.Company_Information.Find(id);
            if (company != null)
            {
                db.Company_Information.Remove(company);
                db.SaveChanges();
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
