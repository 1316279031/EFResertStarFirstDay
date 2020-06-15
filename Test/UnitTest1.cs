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
                Telephone = "17855527769",
                PareventTelephone = "18855579263"
            };
            StudentDetialData stuDetial = new StudentDetialData
                {
                    Name = "李俊",
                    Department = "计算机系",
                    Class = "18网络2班",
                    StudentDatas = studata
                };
                studata.StudentDetialDatas = stuDetial;
                stu.studentDetialDatas.Remove(stuDetial);
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
