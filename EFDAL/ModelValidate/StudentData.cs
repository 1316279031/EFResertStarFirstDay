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
            [Required(ErrorMessage = "请输入身份证号")]
            [RegularExpression(@"^[0-9x]{18}$", ErrorMessage = "您输入的身份证必须正确")]
            public string IdCard { get; set; }
            [Required(ErrorMessage = "请输入家庭地址")]
            public string Address { get; set; }
            [Required(ErrorMessage = "请输入联系号码")]
            [RegularExpression(@"^[0-9]{11}$", ErrorMessage = "输入的联系号码必须在11位数")]
            public string Telephone { get; set; }
            [Required(ErrorMessage = "请输入家长联系号码")]
            [RegularExpression(@"^[0-9]{11}$", ErrorMessage = "输入的联系号码必须在11位数")]
            //[Compare("Telephone",ErrorMessage = "您输入的两次手机号码相同这是不可取的!")]改用customerValidation特性进行验证
            public string PareventTelephone { get; set; }
            public StudentDetialData StudentDetialDatas { get; set; }
        }
    }
}
