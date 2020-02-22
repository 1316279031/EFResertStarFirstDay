using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EFResertStarFirstDay.Models.Filters;
using EFResertStarFirstDay.Models.ModelBLL;

namespace EFResertStarFirstDay.Controllers
{
    public class AdministartorsViewsController : Controller
    {
        [HttpGet] //此页面并不是所有人都可以进入，需要配管理员权限
        [AdministratorsViews]
        public ActionResult Home()
        {
            var administratorObject = Session["AdministratorObject"] as AdministratorObject;
            ViewData["AdministratorName"] = administratorObject.Account;
            ViewData["AdministratorAuthority"] = administratorObject.Authority;
            return View();
        }
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