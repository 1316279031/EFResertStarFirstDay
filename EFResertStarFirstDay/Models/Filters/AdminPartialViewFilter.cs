using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EFResertStarFirstDay.Models.Filters
{
    public class AdminPartialViewFilterAttribute:FilterAttribute,IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var httpContext = filterContext.HttpContext;
            if (httpContext.Session["AdministratorObject"] == null)
            {
                filterContext.Result=new HttpStatusCodeResult(404,"您没有权限查看此页面");
            }
        }
    }
}