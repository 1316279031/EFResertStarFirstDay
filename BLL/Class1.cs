using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using IEFDAL;

namespace BLL
{
    public abstract class SessionBLL<T> where T : class
    {
        private DbContext dbContext;
        public abstract void SetDbSession();

        public SessionBLL()
        {
            SetDbSession();
        }
         public T GetEntityForExpression(Expression<Func<T, bool>> expression)
         {
            return dbContext.Set<T>().FirstOrDefault(expression);
         }

         public T GetGetEntity(string key)
         {
             return null;
         }
    }
}
