using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDAL
{
    public class RegisterAdministartorValidateCode
    {
        public  int Id { get; set; }
        public string ValidateCode { get; set; }
        public virtual SchoolAdministrator SchoolAdministrators { get; set; }
    }

    public class RegisterAdministartorValidateCodeConfig : EntityTypeConfiguration<RegisterAdministartorValidateCode>
    {
        public RegisterAdministartorValidateCodeConfig()
        {
            ToTable("ValidateCodes");
            HasKey(key => key.Id);
            Property(validateCode => validateCode.ValidateCode).IsRequired();
        }
    }
}
