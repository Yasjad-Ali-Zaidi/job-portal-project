using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobPortal.Controllers
{
    public class viewjobsController : Controller
    {
        // GET: viewjobs
        public ActionResult Index()
        {
            return View();
        }
    }
}