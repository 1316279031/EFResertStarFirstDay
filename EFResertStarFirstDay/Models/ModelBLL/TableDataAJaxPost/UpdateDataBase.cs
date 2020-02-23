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
    public class UpdateDataBase:ISchoolTableUpdateDatabase
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
                  var entity=  dal.GetEntity(data.AdministratorAccount);entity.CreateAdminitratorDetialDatas.IsFreeze = data.CreateAdminitratorDetialDatas.IsFreeze;
                    bool isUp= dal.Update(entity);
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