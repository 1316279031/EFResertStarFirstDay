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
using EFResertStarFirstDay.Models.Bll;
using EFResertStarFirstDay.Models.CreateCookie;
using EFResertStarFirstDay.Models.CreateJS;
using EFResertStarFirstDay.Models.Filters;
using EFResertStarFirstDay.Models.ModelBLL;
using EFResertStarFirstDay.Models.ViewModel;

namespace EFResertStarFirstDay.Controllers
{
    public class HomeController : Controller
    {
        private  IErrorDatabaseDal errorDal=new ErrorDatabaseDal(ConfigurationManager.AppSettings["assembly"]);
        private IStudentDetialDataDal dal = new StudentDetialDatasDal(ConfigurationManager.AppSettings["assembly"]);
        private  ISchoolAdministratorDal accountSchoolDal = new SchoolAdministratorDal(ConfigurationManager.AppSettings["assembly"]);
        private IGenerUserDal gerDal = new GenerUserDal(ConfigurationManager.AppSettings["assembly"]);
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
        public ActionResult Login(LogInModel model,string ValidateCode, string Option= "administartor")
        {
            var sessionValidateCode = "";
            var XzPassword = "";
            try
            {
                //登录逻辑代码
                #region 管理员登录代码

                sessionValidateCode = Session["Administartor"] == null ? "" : Session["Administartor"].ToString();
                if (!ComentBll.ExaminationEquals(ValidateCode, sessionValidateCode))
                {
                    ModelState.AddModelError("LogInError", "验证码不正确");
                    return View();
                }
                ILoinValidate genlog = new LoginValidate();
                #region 管理员登录代码
                if (genlog.ValidateAccount(model, Option))
                {
                    var cookie = HttpContext.Request.Cookies["GetValidateTime"];
                    ComentBll.SettingExpiredCookie(HttpContext, cookie);
                    LoginModifySessionData(HttpContext);
                    Session["AdminUserLogin"] = model.Account;
                    //登录的账户与密码验证成功
                    return Redirect("~/AdministartorsViews/Home");
                }
                #endregion
                #endregion
            }
            catch (Exception e)
            {
                ModelState.AddModelError("LogInError", e.Message);
            }
            ModelState.AddModelError("LogInError", "账户名或密码不正确或检查您的登陆选项");
            return View();
        }
        [HttpGet]
        [CookieExpresFilter]
        public ActionResult GetEmailValidateCode(LogInModel model,string ValidateCode, string Option= "administartor")
        {
            bool isValidateForSend = false;
            bool sendIsOk = false;

            //发送验证码
            ICreateEmail sendEmail = new CreateEnail();
            IGetEntity getEntity = new GetEntity();
            //创建四位随机码
            String validateCode = CreateValidateCode.CreateValidateCodes();
            try
            {
                if (model.Account == null || model.Password == null)
                {
                    return JavaScript("");
                }
                Session["Administartor"] = validateCode;
                ILoinValidate log = new LoginValidate();
                if (sendIsOk == false && log.ValidateAccount(model, option: Option))
                {
                    var entity = getEntity.GetEntityForKey(model.Account,
                        accountSchoolDal);
                    sendIsOk = sendEmail.SeendEmail(entity.AdministratorAccount, entity.CreateAdminitratorDetialDatas.Email,
                        validateCode, "登陆验证");
                }
            }
            catch (Exception e)
            {
                isValidateForSend = false;
            }
            if (sendIsOk)
            {
                var createCookie = new CreateCooks();
                //创建Cookie
                var cookie = createCookie.CreateCooki(5);
                HttpContext.Response.Cookies.Add(cookie);
                isValidateForSend = true;
            }
            if (isValidateForSend)
            {
                var str = CreateJavaScript.CreateJS(5);
                return JavaScript(str);
            }
            return new HttpStatusCodeResult(400);
        }

        public void LoginModifySessionData(HttpContextBase httpContext)
        {
            httpContext.Session["XzUserLogin"] = null;
            httpContext.Session["AdminUserLogin"] = null;
            httpContext.Session["GenerUserLogin"] = null;
        }
    }
}