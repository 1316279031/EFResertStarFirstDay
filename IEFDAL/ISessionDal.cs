using System;
using System.Data.Entity;
using System.Reflection;
using EFDAL;

namespace IEFDAL
{

    //接口隔离层
    public interface ISessionDal
    {
        StudentDb GetSessionDbContext(string assemblyName);
    }
    /// <summary>
    /// 创建单实例类型用于返回一个StudentDb的SessionDb对数据库的一个访问操作
    /// </summary>
    public static class SessionDal
    {
        public static StudentDb _sessionDb;
        /// <summary>
        /// 可能引发NullRefenceError
        /// </summary>
        public static StudentDb SessionDb
        {
            get { return _sessionDb; }
            set
            {   
                if (_sessionDb == null)
                {
                    _sessionDb = value;
                }
            }
        }
    }
    public class GetSessionDb : ISessionDal
    {
        public StudentDb GetSessionDbContext(string assemblyName)
        {
            var studentDb = Assembly.Load(assemblyName).CreateInstance(assemblyName + ".StudentDb") as StudentDb;
            //暂时这样做
            return SessionDal._sessionDb = studentDb;
        }
    }
}