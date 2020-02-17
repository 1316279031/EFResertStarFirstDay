using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Test
{
    [TestClass]
    public class UnitTest4
    {
        [TestMethod]
        public void SendCode()
        {
            string Apiurl = "https://api.sendcloud.net/apiv2/mail/send";
            string api_user = "jet_olp_test_AJdX5r";
            string api_key = "pR552vh5BiBlyXjX";
            //HttpClient用于发送http请求
            HttpClient client = null;
            //Http响应消息
            HttpResponseMessage response = null;
            bool statusISOk = true;
            try
            {
                client = new HttpClient();
                List<KeyValuePair<String, String>> paramList = new List<KeyValuePair<String, String>>();
                paramList.Add(new KeyValuePair<string, string>("apiUser", api_user));
                paramList.Add(new KeyValuePair<string, string>("apiKey", api_key));
                paramList.Add(new KeyValuePair<string, string>("from",
                    "Jet@q76zhZsCo6K9y4DY5a9Z9JEYiL1oBfKO.sendcloud.org"));
                paramList.Add(new KeyValuePair<string, string>("fromName", "Jet@YZ"));
                paramList.Add(new KeyValuePair<string, string>("to", "1611855849@qq.com"));
                paramList.Add(new KeyValuePair<string, string>("subject", "管理员注册申请"));
                paramList.Add(new KeyValuePair<string, string>("html",
                    "<p>申请人:" + 1316279031 + "/" + "1611855849@qq.com" + "</p>" + "<p>申请职位:" + "图书管理员" + "</p>" + "<p>申请信息:" + "我申请加入并且我同意相关协议" +
                    "</p>" + "<p>如若同意则点击下方链接生成验证码" + "https://www.sendcloud.net/doc/email_v2/send_email/" + "</p>"));
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
                if (client != null)
                {
                    client.Dispose();
                }
            }

            var str = response.Content.ReadAsStringAsync().Result;
            //根据泛型类型进行反序列化为指定封闭类型对象(2-17根据此方法再次进行一个验证)
           var js= JsonConvert.DeserializeObject<Jsons>(str);
            Console.WriteLine(str);
            Console.WriteLine(js.Message);
            Assert.AreEqual(statusISOk, true);
            //for (int i = 0; i < 5; i++)
            //{
            //    //由于计算机程序在内存中执行速度很快所以种子池相同情况下在执行相同的情况下随机数可能一样
            //    Random ran = new Random(
            //        int.Parse(DateTime.Now.ToString("HHmmssfff"))+i);
            //    ran.Next(1000, 9999);
            //    string empty = String.Empty;
            //    for (int j = 0; j < 4; j++)
            //    {
            //        ran = new Random(int.Parse(DateTime.Now.ToString("hhmmssfff"))+j+i);
            //        var x = ran.Next(65, 90);
            //        char a = (char)x;
            //        empty += a;
            //    }
            //    Console.WriteLine(empty);
            //    Console.WriteLine(Guid.NewGuid().ToString());
            //}
        }
    }

    public class Jsons
    {
        public bool Result { get; set; }
        public string StatusCode { get; set; }
        public string Message { get; set; }
        public object Info { get; set; }
    }
}
