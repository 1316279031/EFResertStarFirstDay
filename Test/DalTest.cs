using System;
using System.Linq;
using DAL;
using EFDAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class DalTest
    {
        [TestMethod]
        public void StudentDatasDalTest()
        {
            StudentDataDal dal = new StudentDataDal("EFDAL");
            bool ors = true;
            if (!(dal!=null))
            {
                ors = false;
                Console.Write("dal==null");
                Assert.AreEqual(ors,true);
            }

           IQueryable<StudentData> data= dal.GetEntityForExpress(x => x.IdCard == "342626200001180199");
            if (data.ToList().Count<=0)
            {
                ors = false;
                Assert.AreEqual(ors, true);
            }
            else
            {
                foreach (var test in data)
                {
                    Console.WriteLine(test.Address);
                    Console.Write(test.IdCard);
                }
            }
        }
    }
}
