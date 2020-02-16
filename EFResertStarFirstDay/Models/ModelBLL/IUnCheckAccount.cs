using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFDAL;
using IEFDAL;

namespace EFResertStarFirstDay.Models.ModelBLL
{
    public interface IUnCheckAccount
    {
        SchoolAdministrator UnCheck(string account, string email, ISchoolAdministratorDal dal);
        bool CreateValidateSeendToEmail(SchoolAdministrator account, string email, IRegisterValidateCodeDal dal,ISchoolAdministratorDal scdal);
    }
}
