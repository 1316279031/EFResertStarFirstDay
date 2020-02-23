using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
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
            var entity=dal.GetEntity("1316279031");
            entity.AdministratorPassword = "123456";
            entity.CreateAdminitratorDetialDatas.CreatedTime=DateTime.Now.AddDays(5);
            entity.CreateAdminitratorDetialDatas.Email = "231231231231@qq.com";
            entity.CreateAdminitratorDetialDatas.IsFreeze = true;
            bool isbool = false;
            isbool=dal.Update(entity);
            Assert.AreEqual(isbool, true);
        }
    }
}
