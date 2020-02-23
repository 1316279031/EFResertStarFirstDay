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
        public ActionResult Login(LogInModel model,string ValidateCode, string Option)
        {
            var sessionValidateCode = "";
            var XzPassword = "";
            try
            {
                //登录逻辑代码
                switch (Option)
                {
                    case "Xz":
                        {
                            #region 校长动态密码校验码登录
                            sessionValidateCode = Session["XzValidate"] == null ? "" : Session["XzValidate"].ToString();
                            XzPassword = Session["XzPassword"] == null ? "" : Session["XzPassword"].ToString(); 
                            if (!ComentBll.ExaminationEquals(ValidateCode, sessionValidateCode))
                            {
                                ModelState.AddModelError("LogInError", "验证码不正确");
                                return View();
                            }
                            if (model.Account == "1316279031" && model.Password != "" && model.Password == XzPassword)
                            {
                                var cookie = HttpContext.Request.Cookies["GetValidateTime"];
                                ComentBll.SettingExpiredCookie(HttpContext, cookie);
                                LoginModifySessionData(HttpContext);
                                Session["XzUserLogin"] = "1316279031";
                                return Redirect("~/AdministartorsViews/Home");
                            }
                            #endregion
                        }; break;
                    case "generUser":
                        {
                        #region 普通用户登录 
                        sessionValidateCode = Session["GenerUser"] == null ? "" : Session["GenerUser"].ToString();
                            if (!ComentBll.ExaminationEquals(ValidateCode, sessionValidateCode))
                            {
                                ModelState.AddModelError("LogInError", "验证码不正确");
                                return View();
                            }
                            ILoinValidate genlog = new LoginValidate();
                            #region 普通用户登录代码
                            if (genlog.ValidateAccount(model, option: Option))//验证:true Ok false Not Ok
                            {
                                var cookie = HttpContext.Request.Cookies["GetValidateTime"];
                                ComentBll.SettingExpiredCookie(HttpContext, cookie);
                                LoginModifySessionData(HttpContext);
                                //登录的账户与密码验证成功
                                Session["GenerUserLogin"] = model.Account;
                                return Content("普通用户登录成功");
                            }
                            #endregion
                        #endregion
                        }; break;
                    case "administartor":
                        {
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
                        }; break;
                    default:; break;
                }
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
        public ActionResult GetEmailValidateCode(LogInModel model,string ValidateCode, string Option )
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
                switch (Option)
            {
                case "Xz":
                {
                        if (model.Account == null)
                        {
                            return JavaScript("");
                        }
                        else
                        {
                            //选择校长邮箱发送随机验证码登录
                            //默认用户
                            if (model.Account == "1316279031")
                            {
                                //动态密码
                                string password = CreateValidateCode.CreateValidateCodes(4);
                                sendIsOk = sendEmail.SeendEmail("1316279031", "1316279031@qq.com",
                                    validateCode, "登陆验证", Password: password, null);
                                //校验码
                                Session["XzValidate"] = validateCode;
                                Session["XzPassword"] = password;
                            }
                        }
                };break;
                case "generUser":
                {
                        if (model.Account == null || model.Password == null)
                        {
                            return JavaScript("");
                        }
                        Session["GenerUser"] = validateCode;
                        ILoinValidate log = new LoginValidate();
                        if (sendIsOk == false && log.ValidateAccount(model, option: Option))
                        {
                                //获取用户
                                var generUser = getEntity.GetEntityForKey(model.Account, gerDal);
                                sendIsOk = sendEmail.SeendEmail(generUser.User, generUser.UserDetial.Email,
                                    validateCode, "登陆验证");
                        }
                };break;
                case "administartor":
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
                } ; break;
                default:;break;
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