using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using EFDAL;
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
    }
}