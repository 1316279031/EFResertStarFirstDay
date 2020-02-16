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
                var rightUrl = Url.Action("SeendValidateCode");
                var LeftUrl = Request.Url.GetLeftPart(UriPartial.Authority) + rightUrl;
                try
                {
                    
                    var entity = dal.GetEntity(schoolAdministrator.AdministratorAccount);
                    LeftUrl = LeftUrl+"?account =" + entity.AdministratorAccount + "&email="+ entity.CreateAdminitratorDetialDatas.Email;
                    ICreateEmail createEmail = new CreateEnail();
                    createEmail.SeendEmail(LeftUrl,entity.CreateAdminitratorDetialDatas.Email, entity.CreateAdminitratorDetialDatas.Message, entity.AdministratorAccount, entity.CreateAdminitratorDetialDatas.AdministratorAuthority);
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
                    ModelState.AddModelError("RegisterError", "验证功能暂时失败请您后续登陆网站发送验证");
                }
             
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
                    ModelState.AddModelError("RegisterError", "您输入的邮箱已被占用");
            }
            if (resiterIS)
            {
                return Content("完成");
            }
            return View("AdministartorsRegister");
        }
        [HttpGet]
        public ActionResult SeendValidateCode(string account, string email)
        {
            //发送验证码给申请者并将验证码保存到数据库中
            //1.检查数据库中是否已存在该账户
            IUnCheckAccount unCheck=new AdministratorRegisterBll();
            var entity = unCheck.UnCheck(account, email, dal);
            if (entity==null)
            {
                return Content("抱歉。关键字不正确！");
            }

            bool Createdal = true;
            try
            {
                IRegisterValidateCodeDal codeDal = new RegisterAdministartorValidateCodeDal(ConfigurationManager.AppSettings["assembly"]);
                var count= codeDal.GetEntityForExpress(x => x.SchoolAdministrators.AdministratorAccount == account).Count();
                if (count > 0)
                {
                    return Content("验证通道暂时关闭");
                }
                var validateSeendBool=unCheck.CreateValidateSeendToEmail(entity, email, codeDal,dal);
                if (validateSeendBool == false)
                {
                    return Content("邮件发送失败");
                }
            }
            catch (Exception e)
            {
                ErrorDatabase error = new ErrorDatabase()
                {
                    DateTime = DateTime.Now,
                    ErrorMessage = e.StackTrace.ToString()
                };
                errorDal.AddEntity(error);
                Createdal = false;
            }
            if (Createdal == false)
            {
                return Content("程序出现了错误！抱歉");
            }
            
            return Content("验证码已发送");
        }
    }
}