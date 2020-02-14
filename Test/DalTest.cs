using System;
using System.Linq;
using System.Security;
using DAL;
using EFDAL;
using IEFDAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class DalTest
    {
        [TestMethod]
        public void AdministartorTest()
        {
            SchoolAdministrator sc =new SchoolAdministrator()
            {
                AdministratorAccount = "1316279031",
                AdministratorPassword = "1314520hw",
                CreateAdminitratorDetialDatas = new CreateAdminitratorDetialData()
                {
                    AdministratorAuthority = "课目管理",
                    CreatedTime = DateTime.Now,
                    Email = "1316279032@qq.com",
                    IsFreeze = false,
                    Message = "嗯嗯",
                    ValidateCode = "4312"
                }
            };
            ISchoolAdministratorDal dal = new SchoolAdministratorDal("EFDAL");
            var num=dal.AddEntity(sc);
            bool bor = false;
            if (num > 0)
            {
                bor = true;
                Console.WriteLine("成功");
            }
            Assert.AreEqual(bor, true);
        }
        //[TestMethod]
        //public void StudentDatasDalTest()
        //{
        //    StudentDataDal dal = new StudentDataDal("EFDAL");
        //    bool ors = true;
        //    if (!(dal!=null))
        //    {
        //        ors = false;
        //        Console.Write("dal==null");
        //        Assert.AreEqual(ors,true);
        //    }

        //   IQueryable<StudentData> data= dal.GetEntityForExpress(x => x.IdCard == "342626200001180199");
        //    if (data.ToList().Count<=0)
        //    {
        //        ors = false;
        //        Assert.AreEqual(ors, true);
        //    }
        //    else
        //    {
        //        foreach (var test in data)
        //        {
        //            Console.WriteLine(test.Address);
        //            Console.Write(test.IdCard);
        //        }
        //    }
        //}
    }
}
