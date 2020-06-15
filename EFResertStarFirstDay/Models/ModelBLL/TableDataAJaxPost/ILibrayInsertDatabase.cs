using EFDAL;
using IEFDAL;
using System.Collections.Generic;

namespace EFResertStarFirstDay.Models.ModelBLL.TableDataAJaxPost
{
    public interface ILibrayInsertDatabase
    {
        bool Insert(IEnumerable<LibrayManagent> datas, ILibrayManagentDAL dal);
    }
}