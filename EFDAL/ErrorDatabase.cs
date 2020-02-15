using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDAL
{
   public class ErrorDatabase
    {
        public int ErrorID { get; set; }
        public DateTime DateTime { get; set; }
        public string ErrorMessage { get; set; }
    }

   public class ErrorDatabaseConfig : EntityTypeConfiguration<ErrorDatabase>
   {
       public ErrorDatabaseConfig()
       {
           ToTable("ErrorDatabases");
           HasKey(x => x.ErrorID);
           Property(x => x.DateTime).IsRequired();
           Property(x => x.ErrorMessage).IsRequired();
       }
   }
}
