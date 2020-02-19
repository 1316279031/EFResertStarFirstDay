using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
    }
}