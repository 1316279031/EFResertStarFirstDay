using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IEFDAL;

namespace EFResertStarFirstDay.Models.ModelBLL
{
   public interface IGetEntity
   {
       T GetEntityForKey<T>(string key, IBaseDal<T> dal) where T : class;
   }
}
