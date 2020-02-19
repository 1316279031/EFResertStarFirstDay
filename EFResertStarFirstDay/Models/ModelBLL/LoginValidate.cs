using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DAL;
using EFDAL;
using EFResertStarFirstDay.Models.ViewModel;
using IEFDAL;

namespace EFResertStarFirstDay.Models.ModelBLL
{
    public class LoginValidate:ILoinValidate
    {
        public bool ValidateAccount(SchoolAdministrator administrator,ISchoolAdministratorDal dal)
        {
            bool IsLog = true;
            bool createdSha256 = true;
            try
            {
                var entity = dal.GetEntity(administrator.AdministratorAccount);
                ICreateSha256Passwrod sha256=new AdministratorRegisterBll();
                try
                {
                    var sha256Password = sha256.CreateSha256Passsword(administrator.AdministratorPassword);
                    if (sha256Password != entity.AdministratorPassword)
                    {
                        IsLog = false;
                    }
                }
                catch (Exception e)
                {
                    throw  new ArgumentException("sha256创建失败");
                    createdSha256 = false;
                }
            }
            catch (Exception e)
            {
                if (createdSha256)
                {
                    throw new NullReferenceException("不存在该账户");
                }
                else
                {
                    throw new ArgumentException(e.Message);
                }
            }
            return IsLog;
        }

        public bool ValidateAccount(LogInModel model, bool option)
        {
            try
            {
                if (option)
                {
                    IGenerUserDal dal = new GenerUserDal(ConfigurationManager.AppSettings["assembly"]);
                   return  Validate(model, genUserdal: dal);
                }
                else
                {
                    ISchoolAdministratorDal dal = new SchoolAdministratorDal(ConfigurationManager.AppSettings["assembly"]);
                   return  Validate(model, schooldal: dal);
                }
            }
            catch (Exception e)
            {
              throw new NullReferenceException("不存在该账户");
            }
        }

        public bool Validate(LogInModel model,ISchoolAdministratorDal schooldal = null,IGenerUserDal genUserdal=null)
        {
            bool IsLog = true;
            var GetEntitypasswrod = "";
            var sha256Password = "";
            ICreateSha256Passwrod sha256 = new AdministratorRegisterBll();
            sha256Password = sha256.CreateSha256Passsword(model.Password);
            if (schooldal == null)//如果schoolDal==null说明我们正在使用普通用户登录
            {
                var entity = genUserdal.GetEntity(model.Account);
                if (entity != null)
                {
                    GetEntitypasswrod = entity.Password;
                }
            }
            else
            {
                var entity = schooldal.GetEntity(model.Account);
                if (entity != null)
                {
                    GetEntitypasswrod = entity.AdministratorPassword;
                }
            }
            if (sha256Password != GetEntitypasswrod)
            {
                IsLog = false;
            }
            return IsLog;
        }
    }
}