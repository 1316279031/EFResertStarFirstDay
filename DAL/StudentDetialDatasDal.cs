﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFDAL;
using IEFDAL;

namespace DAL
{
   public class StudentDetialDatasDal:BaseDal<StudentDetialData>,IStudentDetialDataDal
    {
        public StudentDetialDatasDal(string assembly) : base(assembly)
        {
        }
    }
}
