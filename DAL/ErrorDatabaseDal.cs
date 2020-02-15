using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFDAL;
using IEFDAL;

namespace DAL
{
    public class ErrorDatabaseDal:BaseDal<ErrorDatabase>,IErrorDatabaseDal
    {
        public ErrorDatabaseDal(string assembly) : base(assembly)
        {
        }
    }
}
