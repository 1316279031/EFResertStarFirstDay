using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using IEFDAL;
using System.Configuration;
using EFDAL;
using System.Threading.Tasks;
using System.Net.Http;
using DAL;

namespace EFResertStarFirstDay.Controllers
{
    public class HomeController : Controller
    {
        private IStudentDetialDataDal dal = new StudentDetialDatasDal(ConfigurationManager.AppSettings["assembly"]);
        [HttpGet]
        public ActionResult Index()
        {
            //SelectListItem se= new SelectListItem { 
            //    Text
            //}
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Index(StudentDetialData stuDetialData, StudentData stuData)
        {
            bool access = true;
            try
            {
                if (!ModelState.IsValid)
                {
                  return View();
                }
                stuDetialData.StudentDatas = stuData;
                dal.AddEntity(stuDetialData);
            }
            catch
            {
                access = false;
            }
            StringBuilder builder = new StringBuilder();
            if (access == true)
            {
                builder.Append("<script>alert('保存成功 准备跳转');");
                builder.Append("window.location= '../../Home/Index';</script>");
                return Content(builder.ToString());
            }
            ModelState.AddModelError("validate", "请检查您的身份证是否错误或以添加");
            return View();
        }
    }
}