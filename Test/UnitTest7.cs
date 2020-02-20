using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class UnitTest7
    {
        [TestMethod]
        public void TestMethod1()
        {
            int x = 5;
            int y = 4;
            switch (x)
            {
                case 5:
                {
                    Console.WriteLine("asdasdasdasd");
                    return;
                }break;
            }
            Console.WriteLine("未结束");
        }
    }
}
