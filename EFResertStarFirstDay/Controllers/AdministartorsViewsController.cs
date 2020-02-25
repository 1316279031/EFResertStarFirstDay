using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using DAL;
using EFDAL;
using EFResertStarFirstDay.Models.Filters;
using EFResertStarFirstDay.Models.ModelBLL;
using EFResertStarFirstDay.Models.ModelBLL.TableDataAJaxPost;
using IEFDAL;
using Newtonsoft.Json;

namespace EFResertStarFirstDay.Controllers
{
    public class AdministartorsViewsController : Controller
    {
        [HttpGet] //此页面并不是所有人都可以进入，需要配管理员权限
        [AdministratorsViews]
        public ActionResult Home()
        {
            var administratorObject = Session["AdministratorObject"] as AdministratorObject;
            ViewData["AdministratorName"] = administratorObject.Account;
            ViewData["AdministratorAuthority"] = administratorObject.Authority;
            return View();
        }
        // GET: AdministartorsViews
        [HttpGet]
        [AdminPartialViewFilter]
        public ActionResult XzViews()
        {
            var adminObj = Session["AdministratorObject"] as AdministratorObject;
            var account = adminObj.Account;
            var authority = adminObj.Authority;
            if (authority != "校长")
            {
                return new HttpStatusCodeResult(404, "您没有权限查看此页面");
            }
            IGetEntity get = new GetEntity();
            ISchoolAdministratorDal sc = new SchoolAdministratorDal(ConfigurationManager.AppSettings["assembly"]);
            SchoolAdministrator adminTable = new SchoolAdministrator();
            var jsonData = get.GetEntitys(x => true, sc).ToList();
            ViewData["dd"] = "qweqwe";
            //将对象序列化为JSON格式
           var json= CreateJson(jsonData);
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult XzViews(IEnumerable<SchoolAdministrator> adminDatas)
        {
            //修改管理员数据
            ISchoolTableUpdateDatabase update=new UpdateDataBase();
           var isUpdate=  update.UpData(adminDatas, new SchoolAdministratorDal(ConfigurationManager.AppSettings["assembly"]));
           if (isUpdate)
           {
                //将对象序列化为JSON格式
                var json = CreateJson(adminDatas);
                return Json(json);
           }
           return  new HttpStatusCodeResult(404,"保存失败");
        }
        [HttpGet]
        //请求是学籍管理者的请求(只允许学籍管理者通过这里的请求)为了测试暂时所有权限放开
        public ActionResult StuStatusAdministrator()
        {
            IStudentDetialDataDal dal = new StudentDetialDatasDal(ConfigurationManager.AppSettings["assembly"]);
            List<StudentDetialData> list = new List<StudentDetialData>();
            list = dal.GetEntityForExpress(x => true).ToList();
            //序列化为JSON数据
            var json = CreateJson(list);
            return Json(
                json, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult StuStatusAdministrator(IEnumerable<StudentDetialData> adminDatas)
        {
            IStudentDetialDataDal dal = new StudentDetialDatasDal(ConfigurationManager.AppSettings["assembly"]);
            IStudentUpdateDabase update= new UpdateDataBase();
           bool isUpdate= update.UpData(adminDatas, dal);
           if (isUpdate)
           {
                //将对象序列化为JSON格式
                var json = CreateJson(adminDatas);
                return Json(json);
            }
            return new HttpStatusCodeResult(404,"无法保存");
        }
        public string CreateJson<T>(T obj)
        {
            //JSON序列化配置
            JsonSerializerSettings setting = new JsonSerializerSettings();
            setting.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            setting.Formatting = Formatting.None;
            var str=JsonConvert.SerializeObject(obj,setting);
            return str;
        }
    }
}