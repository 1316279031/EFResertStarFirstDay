using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using IEFDAL;

namespace EFResertStarFirstDay.Models.ModelBLL
{
    public class GetEntity : IGetEntity
    {
        public T GetEntityForKey<T>(string key, IBaseDal<T> dal) where T : class
        {
            return dal.GetEntity(key);
        }

        public IQueryable<T> GetEntitys<T>(Expression<Func<T, bool>> expression, IBaseDal<T> dal) where T : class
        {
            return dal.GetEntityForExpress(expression);
        }
    }
}