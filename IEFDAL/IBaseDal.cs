using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IEFDAL
{
    public interface IBaseDal<T> where T:class
    {
        //R
        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <param name="id">根据主键查找返回</param>
        /// <returns>返回一个T实体</returns>
        T GetEntity(string id);

        /// <summary>
        /// 根据用户条件返回
        /// </summary>
        /// <param name="expression">自定义表达式查找</param>
        /// <returns>返回一组可延迟查找的Iqueryable<T>实体</returns>
        T GetEntity(int id);
        IQueryable<T> GetEntityForExpress(Expression<Func<T, bool>> expression);
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>返回0表示失败，返回值>0表示附加成功</returns>
         int AddEntity(T entity);
        /// <summary>
        /// 修改：Entity自动根据实体跟踪到数据库中的实体进行修改
        /// </summary>
        /// <param name="entity">需要修改的实体</param>
        /// <returns>返回true表示保存成功;返回false表示保存失败</returns>
         bool Update(T entity);
        /// <summary>
        /// 删除:Entity自动根据实体跟踪到数据库中相应实体进行删除
        /// </summary>
        /// <param name="entity">需要进行删除的实体</param>
        /// <returns>返回true表示保存成功;返回fasle表示保存失败</returns>
        bool Delete(T entity);
        /// <summary>
        /// 更新不同属性对象
        /// </summary>
        /// <param name="aimsEntity">需要更新的上下文被追踪的对象</param>
        /// <param name="entity">拥有更新值的对象</param>
        /// <returns></returns>
        bool UpdateToCurrentValuesSets(T aimsEntity, T entity); 
    }
}
