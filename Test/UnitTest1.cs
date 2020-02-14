using System;
using EFDAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
           bool access = true;
           try
            {
                StudentDb stu = new StudentDb();
            StudentData studata = new StudentData
            {
                IdCard = "342626200001180191",
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
                stu.studentDetialDatas.Add(stuDetial);
                stu.SaveChanges();
            }
            catch
            {
               access = false;
               Console.WriteLine("出现错误");
            }
            Assert.AreEqual(access, true);
        }
    }
}
