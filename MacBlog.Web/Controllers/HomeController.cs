using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MacBlog.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Usuario");
            }
            return RedirectToAction("Menu");
        }

        public ActionResult Menu()
        {
            return View();
        }
    }
}