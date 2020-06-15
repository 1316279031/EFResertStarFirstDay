using DAL;
using EFDAL;
using IEFDAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace EFResertStarFirstDay.Models.ModelBLL.TableDataAJaxPost
{
    public class DeleteDatas : ILibrayDeleteDatabase, IStudentDetialDelete
    {
        public bool LibrayDelete(IEnumerable<LibrayManagent> datas, ILibrayManagentDAL dal)
        {
            if (datas == null || dal == null)
            {
                return false;
            }

            bool updateIsFun = true;
            try
            {

                foreach (var data in datas)
                {
                    if (data == null)
                    {
                        continue;
                    }
                    bool isUp = dal.Delete(data);
                }
            }
            catch (Exception e)
            {
                updateIsFun = false;
            }
            return updateIsFun;
        }

        public bool LibrayDelete(IEnumerable<StudentDetialData> datas, IStudentDetialDataDal dal)
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
                    var en=dal.GetEntity(data.ID);
                    var stu = en.StudentDatas;
                    IStudentDataDal dals= new StudentDataDal((ConfigurationManager.AppSettings["assembly"]));
                    dals.Delete(stu);
                    dal.Delete(en);
                }
            }
            catch (Exception e)
            {
                updateIsFun = false;
            }
            return updateIsFun;
        }
    }

    public interface ILibrayDeleteDatabase
    {
         bool LibrayDelete(IEnumerable<LibrayManagent> datas,ILibrayManagentDAL dal);
    }
    public interface IStudentDetialDelete {
        bool LibrayDelete(IEnumerable<StudentDetialData> datas, IStudentDetialDataDal dal);
    }
}