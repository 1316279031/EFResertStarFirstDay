using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFDAL;
using IEFDAL;

namespace DAL
{
   public class CreateAdministratorDetialDataDal:BaseDal<CreateAdminitratorDetialData>,ICreateAdministratorDetialDataDal
    {
        public CreateAdministratorDetialDataDal(string assembly) : base(assembly)
        {
        }
    }
}
