using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using EFDAL;
using EFResertStarFirstDay.Models.ModelBLL;
using IEFDAL;

namespace EFResertStarFirstDay.Controllers
{
    public class AdministartorRegisterController : Controller
    {
        private  IErrorDatabaseDal errorDal=new ErrorDatabaseDal(ConfigurationManager.AppSettings["assembly"]);
        private AdministratorRegisterBll re = new AdministratorRegisterBll();
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

            if (re.DatabaseHasEntity(schoolAdministrator, dal))
            {
                ModelState.AddModelError("RegisterError", "该账户已被占用");
                return View();
            }
            else
            {
                Session["admin"] = schoolAdministrator;
            }
          return View("AdministartorsRegisterDetials");
        }

        [HttpGet]
        public ActionResult AdministartorsRegisterDetials()
        {
            return View("AdministartorsRegister");
        }
        [HttpPost]
        public ActionResult AdministartorsRegisterDetials(CreateAdminitratorDetialData cre)
        {
            var or= Session["admin"] is SchoolAdministrator;
            SchoolAdministrator schoolAdministrator=null;
            if (or == false)
            {
                ModelState.AddModelError("RegisterError", "出现了意外的错误");
                return View("AdministartorsRegister");
            }
                schoolAdministrator = Session["admin"] as SchoolAdministrator;
                bool resiterIS = true;
                try
                {
                    re.RegisterBll(schoolAdministrator, cre, dal);
                    //用户注册后发送一个带QueryString的URL给校长
                }
                catch (Exception e)
                {

                    ErrorDatabase error = new ErrorDatabase()
                    {
                        DateTime = DateTime.Now,
                        ErrorMessage = e.StackTrace.ToString()
                    };
                    errorDal.AddEntity(error);
                    resiterIS = false;
                }
            if (resiterIS)
            {
                return Content("完成");
            }
            ModelState.AddModelError("RegisterError", "您输入的邮箱已被占用");
            return View("AdministartorsRegister");
        }
    }
}