using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDAL
{
    //管理员具体表
   public partial class CreateAdminitratorDetialData
    {
        public int ID { get; set; }
        //权限
        public string AdministratorAuthority { get; set; }
        //该账户权限是否已被冻结
        public bool IsFreeze { get; set; }
        //必须绑定QQ邮箱(发送给校长审核)
        public string Email { get; set; }
        //验证码(1.发送校长审核,2.校长点击确定返回一个验证码3.重新输入验证码接除账户被锁定的一个状态)
        public string ValidateCode
        { get; set; }
        //申请信息
        public string Message { get; set; }
        //创建时间(如果被冻结或未验证(冻结)3天后该账户则会被删除会被别的用户登录 如果期间没有别的用户注册此账户则继续保留)
        public DateTime CreatedTime { get; set; }
        public  virtual SchoolAdministrator SchoolAdministrator { get; set; }
    }

   public class CreateAdminitratorDetialDataConfig : EntityTypeConfiguration<CreateAdminitratorDetialData>
   {
       public CreateAdminitratorDetialDataConfig()
       {
           ToTable("CreateAdminitratorDetialDatas");
           HasKey(x => x.ID);
           Property(x => x.AdministratorAuthority).IsRequired();
            Property(x => x.IsFreeze);
            Property(x => x.Email).IsRequired();
            Property(x => x.Message);
        }
   }
}
