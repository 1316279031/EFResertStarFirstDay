using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using DAL;
using EFDAL;
using IEFDAL;

namespace EFResertStarFirstDay.Models.ModelBLL
{
    public class AdministratorRegisterBll:IUnCheckAccount, ICreateSha256Passwrod
    {
        /// <summary>
        /// 完善管理员注册的系统信息，并将权限冻结
        /// </summary>
        /// <param name="school">SchoolAdministrator</param>
        /// <param name="cre">CreateAdminitratorDetialData</param>
        /// <param name="dal">ISchoolAdministratorDal</param>
        /// <returns>true/false</returns>
        public bool RegisterBll(SchoolAdministrator school, CreateAdminitratorDetialData cre, ISchoolAdministratorDal dal)
        {
            school.AdministratorPassword = CreateSha256Passsword(school.AdministratorPassword);
        cre.CreatedTime=DateTime.Now;
        cre.IsFreeze = false;
        school.CreateAdminitratorDetialDatas = cre;
            var num = dal.AddEntity(school);
            return num > 0;
        }
        /// <summary>
        /// 如果返回True表示数据库中已存在该用户
        /// </summary>
        /// <param name="schoolAdministrator">用户提交的注册账户信息</param>
        /// <param name="dal">访问层</param>
        /// <returns></returns>
        public bool DatabaseHasEntity(SchoolAdministrator schoolAdministrator,ISchoolAdministratorDal dal)
        {
           var entitys= dal.GetEntityForExpress(x => x.AdministratorAccount == schoolAdministrator.AdministratorAccount);
           if (!(entitys.Count()<=0))
           {
               return true;
           }
           return false;
        }
        /// <summary>
        /// 对密码进行sha265加密生成密文
        /// </summary>
        /// <param name="passwrod">密码明文</param>
        /// <returns>返回密文</returns>
        public string CreateSha256Passsword(string passwrod)
        {
            var bytes = Encoding.UTF8.GetBytes(passwrod);
            SHA256 sha = new SHA256CryptoServiceProvider();
            var sha256PaHash = sha.ComputeHash(bytes);
          return BitConverter.ToString(sha256PaHash).Replace("-", "").ToLower();
        }
        /// <summary>
        /// 检查数据库中是否存在该账户且邮箱与其绑定
        /// </summary>
        /// <param name="account"></param>
        /// <param name="email"></param>
        /// <param name="dal"></param>
        /// <returns></returns>
        public SchoolAdministrator UnCheck(string account, string email, ISchoolAdministratorDal dal)
        {
            var entity = dal.GetEntity(account);
            if (entity == null)
            {
                return null;
            }
            if (entity.CreateAdminitratorDetialDatas.Email == email)
            {
                return entity;
            }
            return null;
        }

        public bool CreateValidateSeendToEmail(SchoolAdministrator account, string email, IRegisterValidateCodeDal dal,ISchoolAdministratorDal scdal)
        {
            bool CreateGuidIsTrue = true;
            string guid = "";
            //创建验证码并发送使用全球唯一标识符
            try
            {
               guid = Guid.NewGuid().ToString();
                account.ValidateCodes = new RegisterAdministartorValidateCode()
                {
                    ValidateCode = guid
                };
                scdal.Update(account);
            }
            catch (Exception e)
            {
                IErrorDatabaseDal errorDal = new ErrorDatabaseDal(ConfigurationManager.AppSettings["assembly"]);
                ErrorDatabase error = new ErrorDatabase()
                {
                    DateTime = DateTime.Now,
                    ErrorMessage = e.StackTrace.ToString()
                };
                errorDal.AddEntity(error);
                CreateGuidIsTrue = false;
            }

            if (CreateGuidIsTrue)
            {
                ICreateEmail cre= new CreateEnail();
               var seendOk= cre.SeendEmail(account.AdministratorAccount, email, guid);
               if (!seendOk)
               {
                   CreateGuidIsTrue = false;
               }
            }
            return CreateGuidIsTrue;
        }
    }
}