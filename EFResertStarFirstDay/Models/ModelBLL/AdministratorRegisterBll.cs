using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using EFDAL;
using IEFDAL;

namespace EFResertStarFirstDay.Models.ModelBLL
{
    public class AdministratorRegisterBll
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
    }
}