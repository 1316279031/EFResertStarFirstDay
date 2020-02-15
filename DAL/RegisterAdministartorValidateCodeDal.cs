using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFDAL;
using IEFDAL;

namespace DAL
{
   public class RegisterAdministartorValidateCodeDal:BaseDal<RegisterAdministartorValidateCode>,IRegisterValidateCodeDal
    {
        public RegisterAdministartorValidateCodeDal(string assembly) : base(assembly)
        {
        }
    }
}
