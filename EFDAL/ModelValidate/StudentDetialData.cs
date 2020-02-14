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
            [Required(ErrorMessage = "您必须输入")]
            [RegularExpression(@"^[\u4e00-\u9fa5]{2,5}$", ErrorMessage = "您必须输入中文姓名")]
            public string Name { get; set; }
            [Required(ErrorMessage = "请选择系别")]
            public string Department { get; set; }
            [Required(ErrorMessage = "请输入班级名称")]
            [RegularExpression(@"^[\d]{2}[\u4e00-\u9fa5]{2,8}[\d]{1,2}[\u4e00-\u9fa5]{1}$",ErrorMessage = "请输入正确的班级名称")]
            public string Class { get; set; }
            public virtual StudentData StudentDatas { get; set; }
        }
    }
}
