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
            Random random = new Random(item);
            var str = random.Next().ToString();
            str = str.Substring(0, 4);
        }
    }
}
