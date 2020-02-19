using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFDAL;
using IEFDAL;

namespace DAL
{
    public class GenerUserDal:BaseDal<GenerUser>,IGenerUserDal     {
        public GenerUserDal(string assembly) : base(assembly)
        {
        }
    }
}
