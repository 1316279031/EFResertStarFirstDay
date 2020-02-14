using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EFDAL;
using IEFDAL;

namespace EFResertStarFirstDay.Controllers
{
    public class AdministartorRegisterController : Controller
    {
        private ISessionDal session = new GetSessionDb(); 
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
            var sessionDb=session.GetSessionDbContext(ConfigurationManager.AppSettings["assembly"]);
            return View("AdministartorsRegisterDetials");
        }

        [HttpGet]
        public ActionResult AdministartorsRegisterDetials(CreateAdminitratorDetialData detialData)
        {
            return View();
        }
    }
}