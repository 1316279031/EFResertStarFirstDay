using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EFDAL;
using IEFDAL;

namespace DAL
{
    public class StudentDataDal: BaseDal<StudentData>,IStudentDataDal
    {
        public StudentDataDal(string assembly):base(assembly)
        {
          
        }
    }
}
