using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EFResertStarFirstDay.Models.CreateCookie
{
    public class CreateCooks
    {
        public HttpCookie CreateCooki(int ExpresMinutes)
        {
            HttpCookie cookie = new HttpCookie("GetValidateTime", DateTime.UtcNow.AddMinutes(ExpresMinutes).ToString())
            {
                Expires = DateTime.UtcNow.AddMinutes(ExpresMinutes),
                HttpOnly = true
            };
            return cookie;
        }
    }
}