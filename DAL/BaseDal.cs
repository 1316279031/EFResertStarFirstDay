using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EFDAL;
using IEFDAL;

namespace DAL
{
    public class BaseDal<T> where T:class
    {
        private  ISessionDal dal =new GetSessionDb();
        private DbContext dbContext;
        public  BaseDal (string assembly)
        {
            dbContext = dal.GetSessionDbContext(assembly);
        }
        //R
        //获取单个实体
        public T GetEntity(string id)
        {
            return dbContext.Set<T>().Find(id);
        }
        public T GetEntity(int id)
        {
            return dbContext.Set<T>().Find(id);
        }
        //根据用户条件返回
        public IQueryable<T> GetEntityForExpress(Expression<Func<T, bool>> expression)
        {
            return dbContext.Set<T>().Where(expression).AsQueryable();
        }
        //C
        public int AddEntity(T entity)
        {
            dbContext.Set<T>().Add(entity);
            return dbContext.SaveChanges();
        }
        //U
        public bool Update(T entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            return dbContext.SaveChanges() > 0;
        }
        //aimsEntity上下文中获取来的对象，用于更新数据
        public bool UpdateToCurrentValuesSets(T aimsEntity,T entity)
        {
            dbContext.Entry(aimsEntity).CurrentValues.SetValues(entity);
            return dbContext.SaveChanges()>0;
        }
        //D
        public bool Delete(T entity)
        {
            dbContext.Entry(entity).State = EntityState.Deleted;
            return dbContext.SaveChanges() > 0;
        }
    }
}
