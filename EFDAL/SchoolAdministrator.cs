using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDAL
{
    //学校系统管理员(根据身份限制权限)
   public partial class SchoolAdministrator
   {
        //账户
        public string AdministratorAccount { get; set; }
        //密码
        public string AdministratorPassword { get; set; }

        public virtual CreateAdminitratorDetialData CreateAdminitratorDetialDatas { get; set; }
        public virtual RegisterAdministartorValidateCode ValidateCodes { get; set; }
    }

   public class SchoolAdministratorConfig : EntityTypeConfiguration<SchoolAdministrator>
   {
       public SchoolAdministratorConfig()
       {
           ToTable("AdministratorAccount");
           HasKey(x => x.AdministratorAccount);
           Property(x => x.AdministratorPassword).IsRequired().HasMaxLength(15);
           HasOptional(x => x.ValidateCodes).WithRequired(x => x.SchoolAdministrators).WillCascadeOnDelete(true);
       }
   }
}
