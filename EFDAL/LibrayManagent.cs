using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDAL
{
    public partial class LibrayManagent
    {
        //书号
        public int ID { get; set; }
        public string Name { get; set; }
        //作者
        public string Author { get; set; }
        //上架时间
        public DateTime DataAdded { get; set; }
        //出版社
        public string PublishingHouse { get; set; }
    }
    public class LibrayManagetnConfiguration : EntityTypeConfiguration<LibrayManagent>
    {
        public LibrayManagetnConfiguration()
        {
            ToTable("LibrayManagent");
            HasKey(x => x.ID);
            Property(x => x.Name).IsRequired();
            Property(x => x.Author).IsRequired();
            Property(x => x.DataAdded).IsRequired();
            Property(x => x.PublishingHouse).IsRequired();
        }
    }
}
