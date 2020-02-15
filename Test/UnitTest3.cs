using System;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class UnitTest3
    {
        [TestMethod]
        public void TestMethod1()
        {
            var passwrod = "123123123";
            var bytes = Encoding.UTF8.GetBytes(passwrod);
            SHA256 sha = new SHA256CryptoServiceProvider();
            var sha256PaHash = sha.ComputeHash(bytes);
           var str= BitConverter.ToString(sha256PaHash).Replace("-","").ToLower();
            Console.WriteLine(str);
        }
    }
}
