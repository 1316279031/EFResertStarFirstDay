using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DAL;
using EFDAL;
using IEFDAL;

namespace EFResertStarFirstDay.Models.ModelBLL.TableDataAJaxPost
{
    public class UpdateDataBase:ISchoolTableUpdateDatabase,IStudentUpdateDabase,ILibrayUpdateDatabase
    {
        public bool UpData(IEnumerable<SchoolAdministrator> datas, ISchoolAdministratorDal dal)
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
                    var entity=dal.GetEntity(data.AdministratorAccount);entity.CreateAdminitratorDetialDatas.IsFreeze = data.CreateAdminitratorDetialDatas.IsFreeze;
                    bool isUp= dal.Update(entity);
                }
            }
            catch (Exception e)
            {
                updateIsFun = false;
            }
            return updateIsFun;
        }

        public bool UpData(IEnumerable<StudentDetialData> datas, IStudentDetialDataDal dal)
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
                    var entity = dal.GetEntity(data.ID); 
                    bool isUp = dal.UpdateToCurrentValuesSets(entity,data);
                }
            }
            catch (Exception e)
            {
                updateIsFun = false;
            }
            return updateIsFun;
        }
        public bool UpData(IEnumerable<LibrayManagent> datas, ILibrayManagentDAL dal)
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
                   var entity = dal.GetEntity(data.ID);
                   bool isUp = dal.UpdateToCurrentValuesSets(entity, data);
                }
            }
            catch (Exception e)
            {
                updateIsFun = false;
            }
            return updateIsFun;
        }
    }

    internal interface ILibrayUpdateDatabase
    {
        bool UpData(IEnumerable<LibrayManagent> datas, ILibrayManagentDAL dal);
    }
}