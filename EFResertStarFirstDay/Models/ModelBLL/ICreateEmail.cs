using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFResertStarFirstDay.Models.ModelBLL
{
    public interface ICreateEmail
    {
        /// <summary>
        /// 返回true 发送成功, 返回false发送失败
        /// </summary>
        /// <param name="url">跳转URL</param>
        /// <param name="emailAccount">需要发送到哪一邮箱</param>
        /// <param name="message">发送信息</param>
        /// <param name="account">账户</param>
        /// <param name="authority">权限</param>
        /// <returns></returns>
        bool SeendEmail(string url,string emailAccount,string message,string account,string authority);
        /// <summary>
        /// 返回true 发送成功, 返回false发送失败
        /// </summary>
        /// <param name="account">账户</param>
        /// <param name="email">需要发送到哪一邮箱</param>
        /// <param name="guid">随机码</param>
        /// <returns></returns>
        bool SeendEmail(string account, string email, string guid);
        /// <summary>
        /// 返回true 发送成功, 返回false发送失败
        /// </summary>
        /// <param name="account">账户</param>
        /// <param name="email">需要发送到哪一邮箱</param>
        /// <param name="guid">随机码</param>
        /// <param name="subJect">标题</param>
        /// <returns></returns>
        bool SeendEmail(string account, string email, string guid,string subJect);

        bool SeendEmail(string account, string email, string guid, string subJect, string Password, string acc);
    }
}
