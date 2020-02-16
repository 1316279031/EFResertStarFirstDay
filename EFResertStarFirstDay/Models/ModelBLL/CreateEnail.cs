﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;

namespace EFResertStarFirstDay.Models.ModelBLL
{
    public class CreateEnail:ICreateEmail
    {
        private  string Apiurl = "https://api.sendcloud.net/apiv2/mail/send";
        private string api_user = "jet_olp_test_AJdX5r";
        private string api_key = "pR552vh5BiBlyXjX";
        private string from = "Jet@q76zhZsCo6K9y4DY5a9Z9JEYiL1oBfKO.sendcloud.org";
        //HttpClient用于发送http请求
        private HttpClient client = null;
        private HttpResponseMessage response = null;
        public bool SeendEmail(string url, string emailAccount,string message,string account,string authority)
        {
            //Http响应消息
            bool statusISOk = true;
            try
            {
                client = new HttpClient();
             var paramList= CreateEmailContent("Jet@YZ", emailAccount, "管理员注册申请",
                    "<p>申请人:" + account + "/" + emailAccount + "</p>" + "<p>申请职位:" + authority + "</p>" + "<p>申请信息:" +
                    message + "</p>" + "<p>如若同意则点击下方链接生成验证码" + url + "</p>");

                #region 邮箱内容
                //IList<KeyValuePair<String, String>> paramList = new List<KeyValuePair<String, String>>();
                //paramList.Add(new KeyValuePair<string, string>("apiUser", api_user));
                //paramList.Add(new KeyValuePair<string, string>("apiKey", api_key));
                //paramList.Add(new KeyValuePair<string, string>("from",from));
                //paramList.Add(new KeyValuePair<string, string>("fromName", "Jet@YZ"));
                //paramList.Add(new KeyValuePair<string, string>("to", emailAccount));
                //paramList.Add(new KeyValuePair<string, string>("subject", "管理员注册申请"));
                //paramList.Add(new KeyValuePair<string, string>("html",
                //    "<p>申请人:" + account + "/" + emailAccount + "</p>" + "<p>申请职位:" + authority + "</p>" + "<p>申请信息:" + message +
                //    "</p>" + "<p>如若同意则点击下方链接生成验证码" + url+ "</p>"));
                #endregion

                response = client.PostAsync(Apiurl, new FormUrlEncodedContent(paramList)).Result;
                if (!response.IsSuccessStatusCode)
                {
                    statusISOk = false;
                } 
            }
            catch (Exception e)
            {
                statusISOk = false;
            }
            finally
            {
                if (client!=null)
                {
                    client.Dispose();
                }
            }
            return statusISOk;
        }

        public bool SeendEmail(string account, string email,string guid)
        {
            StringBuilder builder= new StringBuilder();
            builder.Append("<p>尊敬的:" + account + "/" + email + "您好!</p>");
            builder.Append("<P>这是您的激活码:" + guid + "</p>");
           var paramList =CreateEmailContent("Jet@YZ", email, "账号激活验证码",builder.ToString());
            client=new HttpClient();
            response= client.PostAsync(Apiurl, new FormUrlEncodedContent(paramList)).Result;
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
        public  IList<KeyValuePair<string,string>> CreateEmailContent(string fromName, string to ,string subject,string htmlText)
        {
            List<KeyValuePair<String, String>> paramList = new List<KeyValuePair<String, String>>();
            paramList.Add(new KeyValuePair<string, string>("apiUser", api_user));
            paramList.Add(new KeyValuePair<string, string>("apiKey", api_key));
            paramList.Add(new KeyValuePair<string, string>("from",from));
            paramList.Add(new KeyValuePair<string, string>("fromName", fromName));
            paramList.Add(new KeyValuePair<string, string>("to", to));
            paramList.Add(new KeyValuePair<string, string>("subject", subject));
            paramList.Add(new KeyValuePair<string, string>("html", htmlText));
            return paramList;
        }
    }
}