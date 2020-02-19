using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDAL
{
    [MetadataType(typeof(StudentDetialDataMD))]
    public partial class StudentDetialData
    {
        public class StudentDetialDataMD
        {
            public int ID { get; set; }
            [Required(ErrorMessage = "必须填入姓名")]
            public string Name { get; set; }
            [Required(ErrorMessage = "不许填入系别")]
            public string Department { get; set; }
            [Required(ErrorMessage = "必须填入班级名称")]
            public string Class { get; set; }
        }
    }
}
