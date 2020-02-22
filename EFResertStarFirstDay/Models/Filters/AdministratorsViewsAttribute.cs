using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using DAL;
using EFResertStarFirstDay.Models.ModelBLL;
using IEFDAL;

namespace EFResertStarFirstDay.Models.Filters
{
    public class AdministratorsViewsAttribute:FilterAttribute,IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var httpContext = filterContext.HttpContext;
            if (!AuthorizationRequest(httpContext))
            {
                var leftUrl = httpContext.Request.Url.GetLeftPart(UriPartial.Authority);
                leftUrl += "/Home/Login";
                filterContext.Result = new RedirectResult(leftUrl);
            }
        }

        public bool AuthorizationRequest(HttpContextBase httpContext)
        {
            if (httpContext.Session["XzUserLogin"] == null && httpContext.Session["AdminUserLogin"] == null)
            {
                return false;
            }
            else
            {
                if (httpContext.Session["XzUserLogin"] != null)
                {
                    var ad = new AdministratorObject
                    {
                        Account = httpContext.Session["XzUserLogin"].ToString(),
                        Authority = "校长"
                    };
                    httpContext.Session["AdministratorObject"] = ad;
                }
                else
                {
                    ISchoolAdministratorDal administratorDal = new SchoolAdministratorDal(ConfigurationManager.AppSettings["assembly"]);
                     IGetEntity getEntity = new GetEntity();
                   var entity=  getEntity.GetEntityForKey(httpContext.Session["AdminUserLogin"].ToString(), administratorDal);
                    var ad = new AdministratorObject()
                    {
                        Account = httpContext.Session["AdminUserLogin"].ToString(),
                        Authority = entity.CreateAdminitratorDetialDatas.AdministratorAuthority,
                    };
                    httpContext.Session["AdministratorObject"] = ad;
                }
                return true;
            }
        }
    }
}