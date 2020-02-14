using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EFDAL
{
    //任务任然需要进一部的学习EntityFramework
    public class StudentDb : DbContext
    {
        public StudentDb()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<StudentDb>());
        }
        public DbSet<StudentData> StudentDatas{
            get;set;
        }
        public DbSet<StudentDetialData> studentDetialDatas{
            get;set;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var configColl = Assembly.GetExecutingAssembly().GetTypes()
                .Where(types =>!string.IsNullOrEmpty(types.Namespace))
                .Where(types => types.BaseType != null && types.BaseType.IsGenericType&&
     types.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            foreach (var item in configColl)
            {
                dynamic dy = Activator.CreateInstance(item);                      
                modelBuilder.Configurations.Add(dy);
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}
