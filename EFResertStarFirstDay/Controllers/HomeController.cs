using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using IEFDAL;
using System.Configuration;
using EFDAL;
using DAL;
using EFResertStarFirstDay.Models.CreateCookie;
using EFResertStarFirstDay.Models.CreateJS;
using EFResertStarFirstDay.Models.Filters;
using EFResertStarFirstDay.Models.ModelBLL;

namespace EFResertStarFirstDay.Controllers
{
    public class HomeController : Controller
    {
        private  IErrorDatabaseDal errorDal=new ErrorDatabaseDal(ConfigurationManager.AppSettings["assembly"]);
        private IStudentDetialDataDal dal = new StudentDetialDatasDal(ConfigurationManager.AppSettings["assembly"]);
        private  ISchoolAdministratorDal accountSchoolDal = new SchoolAdministratorDal(ConfigurationManager.AppSettings["assembly"]);
        [HttpGet]
        public ActionResult Index()
        {
            //SelectListItem se= new SelectListItem { 
            //    Text
            //}
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Index(StudentDetialData stuDetialData, StudentData stuData)
        {
            bool access = true;
            try
            {
                if (!ModelState.IsValid)
                {
                  return View();
                }
                stuDetialData.StudentDatas = stuData;
                dal.AddEntity(stuDetialData);
            }
            catch(Exception e)
            {
                ErrorDatabase error = new ErrorDatabase()
                {
                    DateTime = DateTime.Now,
                    ErrorMessage = e.Message
                };
                errorDal.AddEntity(error);
                access = false;
            }
            StringBuilder builder = new StringBuilder();
            if (access == true)
            {
                builder.Append("<script>alert('保存成功 准备跳转');");
                builder.Append("window.location= '../../Home/Index';</script>");
                return Content(builder.ToString());
            }
            ModelState.AddModelError("validate", "请检查您的身份证是否错误或以添加");
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        //如果cookie已过期则将session保存的验证码置0
        [LogInCookieTimeValidate]
        [ValidateAntiForgeryToken]
        public ActionResult Login(SchoolAdministrator administrator,string ValidateCode)
        {
           var sessionValidateCode = Session["Validate"]==null?"": Session["Validate"].ToString();
            if (sessionValidateCode != ValidateCode)
            {
                ModelState.AddModelError("LogInError", "验证码不正确");
                return View();
            }
            ILoinValidate log = new LoginValidate();
            try
            {
                if (log.ValidateAccount(administrator, accountSchoolDal))
                {
                    //登录的账户与密码验证成功
                    return Content("登录成功");
                }
                else
                {
                    //登录的账户与密码验证不成功
                    ModelState.AddModelError("LogInError","账户名或密码不正确");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("LogInError", e.Message);
            }
            return View();
        }
        [HttpGet]
        [CookieExpresFilter]
        public ActionResult GetEmailValidateCode(SchoolAdministrator administrator)
        {
            bool isValidateForSend = true;
            if (administrator.AdministratorAccount == null||administrator.AdministratorPassword==null)
            {
                return JavaScript("");
            }
            ILoinValidate log = new LoginValidate();
            try
            {
                if (log.ValidateAccount(administrator, accountSchoolDal))
                {
                    //创建四位随机码
                    String validateCode = CreateValidateCode.CreateValidateCodes();
                    Session["Validate"] = validateCode;
                    var createCookie = new CreateCooks();
                    //创建Cookie
                    var cookie = createCookie.CreateCooki(5);
                    HttpContext.Response.Cookies.Add(cookie);
                    
                    //发送验证码
                    ICreateEmail sendEmail=new CreateEnail();
                    IGetEntity getEntity=new GetEntity(); 
                var entity=getEntity.GetEntityForKey<SchoolAdministrator>(administrator.AdministratorAccount,
                        accountSchoolDal);
                 bool sendIsOk= sendEmail.SeendEmail(entity.AdministratorAccount, entity.CreateAdminitratorDetialDatas.Email,
                        validateCode,"登陆验证");
                 if (sendIsOk)
                 {
                        isValidateForSend = true;
                 }
                 else
                 {
                     isValidateForSend = false;
                 }
                }
                else
                {
                    isValidateForSend = false;
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("LogInError", "您的账户无法发送验证码");
                isValidateForSend = false;
            }
            if (isValidateForSend)
            {
                var str = CreateJavaScript.CreateJS(5);
                return JavaScript(str);
            }
            return JavaScript("");
        }
    }
}