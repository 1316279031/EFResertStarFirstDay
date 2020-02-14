using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDAL
{
    public partial class StudentData
    {
        public string IdCard { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string PareventTelephone { get; set; }
        public StudentDetialData StudentDetialDatas { get; set; }
    }
    public class StudentDataConfig : EntityTypeConfiguration<StudentData>
    {
        public StudentDataConfig()
        {
            ToTable("StudentDataConfigs");
            HasKey(x => x.IdCard);
            Property(pro => pro.Address).IsRequired();
            Property(pro => pro.Telephone).IsRequired().HasMaxLength(11);
            Property(pro => pro.PareventTelephone).IsRequired().HasMaxLength(11);
            HasRequired(hr => hr.StudentDetialDatas).WithRequiredDependent(wr => wr.StudentDatas);
        }
    }
}
