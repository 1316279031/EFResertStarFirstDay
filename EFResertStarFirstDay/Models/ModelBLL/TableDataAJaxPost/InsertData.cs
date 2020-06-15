using EFDAL;
using IEFDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EFResertStarFirstDay.Models.ModelBLL.TableDataAJaxPost
{
    public class InsertData : ILibrayInsertDatabase
    {
        public bool Insert(IEnumerable<LibrayManagent> datas, ILibrayManagentDAL dal)
        {
            if (datas == null || dal == null)
            {
                return false;
            }

            bool updateIsFun = true;
            try
            {
                //删除StudentDetialData表必须将其从表中的依赖关系删除，我们需要找出所有关系数据，删除。
                foreach (var data in datas)
                {
                    if (data == null)
                    {
                        continue;
                    }
                    dal.AddEntity(data);
                }
            }
            catch (Exception e)
            {
                updateIsFun = false;
            }
            return updateIsFun;
        }
    }
}