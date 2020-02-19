using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDAL
{
    [MetadataType(typeof(StudentDataMD))]
   public partial class StudentData
    {
        public class StudentDataMD
        {
            [Required(ErrorMessage = "必须填入身份证")]
            public string IdCard { get; set; }
            [Required(ErrorMessage = "必须填入住址")]
            public string Address { get; set; }
            [Required(ErrorMessage = "必须填入手机号码")]
            public string Telephone { get; set; }
            [Required(ErrorMessage = "必须填入家长手机号码")]
            public string PareventTelephone { get; set; }
        }
    }
} 
