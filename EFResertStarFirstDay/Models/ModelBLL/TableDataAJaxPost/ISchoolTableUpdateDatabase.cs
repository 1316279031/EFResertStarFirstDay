using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFDAL;
using IEFDAL;

namespace EFResertStarFirstDay.Models.ModelBLL.TableDataAJaxPost
{
    public interface ISchoolTableUpdateDatabase
    {
        bool UpData(IEnumerable<SchoolAdministrator> datas, ISchoolAdministratorDal dal);
    }
}
