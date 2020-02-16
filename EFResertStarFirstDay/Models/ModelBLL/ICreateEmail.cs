using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFResertStarFirstDay.Models.ModelBLL
{
    public interface ICreateEmail
    {
        bool SeendEmail(string url,string emailAccount,string message,string account,string authority);
        bool SeendEmail(string account, string email, string guid);
    }
}
