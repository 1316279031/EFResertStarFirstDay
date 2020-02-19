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
    }
}