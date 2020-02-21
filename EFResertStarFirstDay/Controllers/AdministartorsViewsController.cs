using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EFResertStarFirstDay.Controllers
{
    public class AdministartorsViewsController : Controller
    {
        // GET: AdministartorsViews
        public ActionResult XzViews()
        {
            return View();
        }

        [HttpPost]
        public ActionResult XzViews(string [] args)
        {
            return View();
        }
    }
}