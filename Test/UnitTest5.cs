using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class UnitTest5
    {
        [TestMethod]
        public void Randoms()
        {
            var bytes = Guid.NewGuid().ToByteArray();
            var item = BitConverter.ToInt32(bytes, 0);
            string str = "";
            Random random =null;
            for (int i = 0; i < 4; i++)
            {
                random = new Random(Convert.ToInt32(DateTime.Now.ToString("hhmmfff"+i)));
                if (i % 2 == 0)
                {
                    str += ((char)random.Next(65, 90)).ToString();
                    continue;
                }
                str += ((char)random.Next(97, 122)).ToString();
            }
            random=new Random(item);
            str+= random.Next().ToString();
            Console.WriteLine(str);
        }
    }
}
