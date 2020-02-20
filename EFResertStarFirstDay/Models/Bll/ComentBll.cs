using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EFResertStarFirstDay.Models.Bll
{
    public static class ComentBll
    {
        public static bool ExaminationEquals(string inputValidate,string sessionValidateCode)
        {
            if (inputValidate.Length < 4 || inputValidate != sessionValidateCode)
            {
                return false;
            }

            return true;
        }
        public static void SettingExpiredCookie(HttpContextBase httpContext, HttpCookie cookie)
        {
            cookie.Expires = DateTime.UtcNow.AddHours(-24);
            cookie.Value = DateTime.UtcNow.AddHours(-24).ToString();
            cookie.HttpOnly = true;
            httpContext.Response.Cookies.Add(cookie);
        }
    }
}