using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EFDAL;

namespace EFResertStarFirstDay.Controllers
{
    public class AdministartorRegisterController : Controller
    {
        // GET: AdministartorRegister
        public ActionResult AdministartorsRegister()
        {
            //异步执行 返回一个具体注册信息
            return View();
        }
        [HttpPost]
        public ActionResult AdministartorsRegister(SchoolAdministrator schoolAdministrator)
        {
            return Content("qweqweqweqwe");
        }
    }
}