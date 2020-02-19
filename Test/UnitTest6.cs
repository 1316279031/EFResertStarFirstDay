using System;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class UnitTest6
    {
        [TestMethod]
        public void CookieTest()
        {
            var token = Guid.NewGuid().ToString();
            HttpCookie cookie = new HttpCookie("GetValidateTime", token)
            {
                Expires = DateTime.UtcNow.AddMinutes(5),
                HttpOnly = true,
            };
            Console.WriteLine("本机时间"+"\n"+DateTime.UtcNow.Ticks);
            Console.WriteLine("cookie时间"+"\n"+cookie.Expires.Ticks);
          
            Console.WriteLine(DateTime.UtcNow);
            Console.WriteLine(cookie.Expires);
            if (DateTime.UtcNow < cookie.Expires)
            {
                Console.WriteLine(true);
            }
        }
    }
}
