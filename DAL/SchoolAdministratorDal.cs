using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFDAL;
using IEFDAL;

namespace DAL
{
   public class SchoolAdministratorDal:BaseDal<SchoolAdministrator>,ISchoolAdministratorDal
    {
        public SchoolAdministratorDal(string assembly) : base(assembly)
        {

        }
    }
}
