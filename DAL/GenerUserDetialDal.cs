using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFDAL;
using IEFDAL;

namespace DAL
{
    public class GenerUserDetialDal:BaseDal<GenerUserDetial>,IGenerUserDetialDal
    {
        public GenerUserDetialDal(string assembly) : base(assembly)
        {

        }
    }
}
