using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using EFDAL;
using IEFDAL;

namespace EFResertStarFirstDay.Controllers
{
    public class AdministartorRegisterController : Controller
    {
        private ISchoolAdministratorDal dal=new SchoolAdministratorDal(ConfigurationManager.AppSettings["assembly"]);
        // GET: AdministartorRegister
        public ActionResult AdministartorsRegister()
        {
            //异步执行 返回一个具体注册信息
            return View();
        }
        [HttpPost]
        public ActionResult AdministartorsRegister(SchoolAdministrator schoolAdministrator)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("RegisterError","请检查您的登录信息");
                return View();
            }
             Session["admin"] = schoolAdministrator;
          return View("AdministartorsRegisterDetials");
        }
        [HttpGet]
        public ActionResult AdministartorsRegisterDetials()
        {
            return Redirect("AdministartorsRegister");
        }
        [HttpPost]
        public ActionResult AdministartorsRegisterDetials(CreateAdminitratorDetialData cre)
        {
            var or= Session["admin"] is SchoolAdministrator;
            SchoolAdministrator schoolAdministrator=null;
            if (or == true)
            {
                schoolAdministrator = Session["admin"] as SchoolAdministrator;
            }
            else
            {
                ModelState.AddModelError("RegisterError","出现了意外的错误");
                return View("AdministartorsRegister");
            }
            return Content(schoolAdministrator.AdministratorAccount);
            //return View();
        }
    }
}