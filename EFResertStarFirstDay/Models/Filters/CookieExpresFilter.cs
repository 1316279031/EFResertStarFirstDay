using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using DAL;
using EFResertStarFirstDay.Models.ModelBLL;
using IEFDAL;

namespace EFResertStarFirstDay.Models.Filters
{
    public class LogInCookieTimeValidate : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var httpContext = filterContext.HttpContext;
            if (!httpContext.Request.Cookies.AllKeys.Contains("GetValidateTime"))
            {
                httpContext.Session["Administartor"] = "";
                httpContext.Session["GenerUser"] = "";
                httpContext.Session["XzValidate"] = "";
                httpContext.Session["XzPassword"] = "";
                return;
            }
            var cookie = httpContext.Request.Cookies["GetValidateTime"];
            if (DateTime.UtcNow.Ticks > Convert.ToDateTime(cookie.Value).Ticks)
            {
                cookie.Expires = DateTime.UtcNow.AddHours(-24);
                cookie.HttpOnly = true;
                httpContext.Response.Cookies.Add(cookie);
                httpContext.Session["Administartor"] = "";
                httpContext.Session["GenerUser"] = "";
                httpContext.Session["Xz"] = "";
            }
        }
    }
    public class CookieExpresFilterAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var httpContext = filterContext.HttpContext;
            if (!httpContext.Request.Cookies.AllKeys.Contains("GetValidateTime"))
            {
                return;
            }
            var cookie = httpContext.Request.Cookies["GetValidateTime"];
            if (DateTime.UtcNow.Ticks< Convert.ToDateTime(cookie.Value).Ticks)
            {
                if (httpContext.Request.IsAjaxRequest())
                {
                    filterContext.Result = new HttpStatusCodeResult(504, "请求失败");
                }
            }
        }
    }
}