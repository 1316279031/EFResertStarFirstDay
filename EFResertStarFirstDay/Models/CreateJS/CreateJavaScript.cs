using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace EFResertStarFirstDay.Models.CreateJS
{
    public static class CreateJavaScript
    {
        public static string CreateJS(int mill)
        {
            #region Js Str
            StringBuilder str = new StringBuilder();
            str.Append("(function() {");
            str.Append("let count = "+(mill*60).ToString()+";" +
                       "let href = $('.getvalidateCode').attr('href');" +
                       "let text = $('.getvalidateCode').text(); clock();");
            str.Append("let timeOut = setInterval(clock, 1000);" +
                       "function clock() {" +
                       " $('.getvalidateCode').text(count);" +
                       "$('.getvalidateCode').attr('href', '#');" +
                       "if (count === 0) " +
                       "{" +
                       "ResterClock();" +
                       "return; " +
                       "}" +
                       "count--;" +
                       "}");
            str.Append("function ResterClock() {" +
                       "clearInterval(timeOut);" +
                       "$('.getvalidateCode').text('获取验证码');" +
                       "$('.getvalidateCode').attr('href', href);" + "getValidate.IsRequest=false; }");
            str.Append("}())");
            #endregion

            return str.ToString();
        }
    }
}