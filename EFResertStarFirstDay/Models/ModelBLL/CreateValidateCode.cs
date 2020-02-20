using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EFResertStarFirstDay.Models.ModelBLL
{
    public static class CreateValidateCode
    {
        public static string CreateValidateCodes()
        {
            var bytes = Guid.NewGuid().ToByteArray();
            var item = BitConverter.ToInt32(bytes, 0);
            Random random = new Random(item);
            var str = random.Next().ToString();
            str = str.Substring(0, 4);
            return str;
        }
        public static string CreateValidateCodes(int length=4)
        {
            var bytes = Guid.NewGuid().ToByteArray();
            var item = BitConverter.ToInt32(bytes, 0);
            string str = "";
            Random random = null;
            for (int i = 0; i < length; i++)
            {
                random = new Random(Convert.ToInt32(DateTime.Now.ToString("hhmmfff" + i)));
                if (i % 2 == 0)
                {
                    str += ((char)random.Next(65, 90)).ToString();
                    continue;
                }
                str += ((char)random.Next(97, 122)).ToString();
            }
            random = new Random(item);
            str += random.Next().ToString();
            return str;
        }
    }
}