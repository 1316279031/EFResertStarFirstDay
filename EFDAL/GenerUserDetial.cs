using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDAL
{
    public partial class GenerUserDetial
    {
        public int ID { get; set; }
        //设置索引
        public string Name { get; set; }
        public string Email { get; set; }
        //可以为空以邮箱验证为主 
        public string TelePhone { get; set; }
        public virtual GenerUser Users{ get; set; }
    }

    public class GenerUserDetialConfig : EntityTypeConfiguration<GenerUserDetial>
    {
        public GenerUserDetialConfig()
        {
            ToTable("GenerUserDetials");
            HasKey(x => x.ID);
            Property(x => x.Email);
            Property(x => x.Name).IsRequired();
            Property(x => x.TelePhone);
        }
    }
}
