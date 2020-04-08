using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace CompanyDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Company");
        }

        public ActionResult ProductList()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult ProductReport()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}