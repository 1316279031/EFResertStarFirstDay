using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDAL
{
    public partial class StudentDetialData
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Department { get;set; }
        public string Class { get; set; }
        public virtual StudentData StudentDatas { get;set; }
    }

   
    public class StudentDetialDataConfig : EntityTypeConfiguration<StudentDetialData> 
        
        {
        public StudentDetialDataConfig() {
            ToTable("StudentDetialDatas");
            HasKey(key => key.ID);
            Property(p => p.Name);
            Property(p => p.Department);
            Property(p => p.Class).IsRequired();
            HasRequired(has => has.StudentDatas).WithRequiredPrincipal(wd => wd.StudentDetialDatas).WillCascadeOnDelete(true);
        }
    }
}
