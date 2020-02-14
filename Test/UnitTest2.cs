using System;
using DAL;
using EFDAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestMethod1()
        {
            StudentDataDal dal = new StudentDataDal("EFDAL");
            StudentData studata = new StudentData
            {
                IdCard = "342626200001180199",
                Address = "安徽马鞍山",
                Telephone = "18855579263",
                PareventTelephone = "18315508706"
            };
            StudentDetialData stuDetial = new StudentDetialData
            {
                Name = "黄伟",
                Department = "计算机系",
                Class = "18网络2班",
                StudentDatas = studata
            };
            studata.StudentDetialDatas = stuDetial;
            var or=0;
            try
            {
                or = dal.AddEntity(studata);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            bool ors = true;
            if (or >0)
            {
                 ors = true;
            }
            else
            {
                 ors = false;
            }
            Assert.AreEqual(ors, true);
        }
    }
}
