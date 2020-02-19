using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFDAL;
using EFResertStarFirstDay.Models.ViewModel;
using IEFDAL;

namespace EFResertStarFirstDay.Models.ModelBLL
{
    public interface ILoinValidate
    {
        bool ValidateAccount(SchoolAdministrator administrator, ISchoolAdministratorDal dal);
        bool ValidateAccount(LogInModel model, bool option);
    }
}
