using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDAL
{
    public partial class GenerUser
    {
        public string User { get; set; }
        public string Password { get; set; }
        public virtual GenerUserDetial UserDetial{ get; set; }
    }

    public class GenerUserConfig : EntityTypeConfiguration<GenerUser>
    {
        public GenerUserConfig()
        {
            ToTable("GenerUsers");
            HasKey(x => x.User);
            Property(x => x.Password).IsRequired();
            HasOptional(x => x.UserDetial).WithRequired(x => x.Users).WillCascadeOnDelete(true);
        }
    }
}
