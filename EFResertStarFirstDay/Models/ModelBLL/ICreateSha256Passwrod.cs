using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFResertStarFirstDay.Models.ModelBLL
{
    interface ICreateSha256Passwrod
    {
       string CreateSha256Passsword(string passwrod);
    }
}
