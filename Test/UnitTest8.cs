using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using DAL;
using EFDAL;
using IEFDAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class UnitTest8
    {
        [TestMethod]
        public void EfUpdate()
        {
            ISchoolAdministratorDal dal = new SchoolAdministratorDal("EFDAL");
            StudentDb stu = new StudentDb();
            var entity =stu.SchoolAdministrators.FirstOrDefault(x=>x.AdministratorAccount=="1316279031");
            //entity.AdministratorPassword = "123456";
            //entity.CreateAdminitratorDetialDatas.CreatedTime = DateTime.Now.AddDays(5);
            //entity.CreateAdminitratorDetialDatas.Email = "231231231231@qq.com";
            //entity.CreateAdminitratorDetialDatas.IsFreeze = true;
            bool isbool = false;
            SchoolAdministrator sc= new SchoolAdministrator()
            {
                AdministratorAccount = "1316279031",
                AdministratorPassword = "huangwei0118",
                CreateAdminitratorDetialDatas = new CreateAdminitratorDetialData()
                {
                    AdministratorAuthority = "校长",
                    CreatedTime = DateTime.Now.Date,
                    Email = "21231231@qq.com",
                    IsFreeze = false,
                    Message = "Heelo"
                }
            };
            Console.WriteLine(entity.AdministratorAccount);
            //isbool=dal.Update(entity);
            stu.Entry(entity).CurrentValues.SetValues(sc);
            isbool = stu.SaveChanges() > 0;
            Assert.AreEqual(isbool, true);
        }
    }
}
