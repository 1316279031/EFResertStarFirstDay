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
            if (authority != "学籍管理")
            {
                return new HttpStatusCodeResult(404, "您没有权限查看此页面");
            }
            IGetEntity get = new GetEntity();
            ISchoolAdministratorDal sc = new SchoolAdministratorDal(ConfigurationManager.AppSettings["assembly"]);
            SchoolAdministrator adminTable = new SchoolAdministrator();
            var jsonData = get.GetEntitys(x => true, sc).ToList();
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
        //修改
        [HttpPost]
        public ActionResult StuStatusAdministrator(IEnumerable<StudentDetialData> adminDatas)
        {
            IStudentDetialDataDal dal = new StudentDetialDatasDal(ConfigurationManager.AppSettings["assembly"]);
            IStudentUpdateDabase update= new UpdateDataBase();
           bool isUpdate= update.UpData(adminDatas, dal);
           if (isUpdate)
           {
                List<StudentDetialData> list = new List<StudentDetialData>();
                list = dal.GetEntityForExpress(x => true).ToList();
                //序列化为JSON数据
                var json = CreateJson(list);
                return Json(
                    json, JsonRequestBehavior.AllowGet);
            }
            return new HttpStatusCodeResult(404,"无法保存");
        }
        //学籍管理的删除
        [HttpPost]
        public ActionResult StuStatusDeleteAdmin(IEnumerable<StudentDetialData> adminDatas) {
            IStudentDetialDataDal dal = new StudentDetialDatasDal(ConfigurationManager.AppSettings["assembly"]);
            IStudentDetialDelete delete = new DeleteDatas();
            bool isUpdate = delete.LibrayDelete(adminDatas, dal);
            if (isUpdate)
            {
                IGetEntity get = new GetEntity();
                List<StudentDetialData> list = new List<StudentDetialData>();
                list = dal.GetEntityForExpress(x => true).ToList();
                //序列化为JSON数据
                var json = CreateJson(list);
                return Json(
                    json, JsonRequestBehavior.AllowGet);
            }
            return new HttpStatusCodeResult(404, "无法保存");
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
        //图书管理界面
        [HttpGet]
        public ActionResult LibrayManagent() {
            var adminObj = Session["AdministratorObject"] as AdministratorObject;
            var account = adminObj.Account;
            var authority = adminObj.Authority;
            if (authority != "图书管理")
            {
                return new HttpStatusCodeResult(404, "您没有权限查看此页面");
            }
            IGetEntity get = new GetEntity();
            ILibrayManagentDAL sc = new LibrayManagetnDal(ConfigurationManager.AppSettings["assembly"]);
            LibrayManagent adminTable = new LibrayManagent();
            var jsonData = get.GetEntitys(x => true, sc).ToList();
            //将对象序列化为JSON格式
            var json = CreateJson(jsonData);
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        //修改
        [HttpPost]
        public ActionResult LibrayManagent(IEnumerable<LibrayManagent> adminDatas)
        {
            ILibrayManagentDAL dal = new LibrayManagetnDal(ConfigurationManager.AppSettings["assembly"]);
            ILibrayUpdateDatabase update = new UpdateDataBase();
            bool isUpdate = update.UpData(adminDatas, dal);
            if (isUpdate)
            {
                IGetEntity get = new GetEntity();
                ILibrayManagentDAL sc = new LibrayManagetnDal(ConfigurationManager.AppSettings["assembly"]);
                LibrayManagent adminTable = new LibrayManagent();
                var jsonData = get.GetEntitys(x => true, sc).ToList();
                //将对象序列化为JSON格式
                var json = CreateJson(jsonData);
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            return new HttpStatusCodeResult(404, "无法保存");
        }
        //删除
        [HttpPost]
        public ActionResult LibrayManagentDelete(IEnumerable<LibrayManagent> adminDatas) {
            ILibrayManagentDAL dal = new LibrayManagetnDal(ConfigurationManager.AppSettings["assembly"]);
            ILibrayDeleteDatabase update = new DeleteDatas();
            bool isUpdate = update.LibrayDelete(adminDatas, dal);
            if (isUpdate)
            {
                IGetEntity get = new GetEntity();
                ILibrayManagentDAL sc = new LibrayManagetnDal(ConfigurationManager.AppSettings["assembly"]);
                LibrayManagent adminTable = new LibrayManagent();
                var jsonData = get.GetEntitys(x => true, sc).ToList();
                //将对象序列化为JSON格式
                var json = CreateJson(jsonData);
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            return new HttpStatusCodeResult(404, "无法保存");
        }
        //插入
        public ActionResult InserLibrayManagent(IEnumerable<LibrayManagent> adminDatas) {
            ILibrayManagentDAL dal = new LibrayManagetnDal(ConfigurationManager.AppSettings["assembly"]);
            ILibrayInsertDatabase update = new InsertData();
            bool isUpdate = update.Insert(adminDatas, dal);
            if (isUpdate)
            {
                IGetEntity get = new GetEntity();
                ILibrayManagentDAL sc = new LibrayManagetnDal(ConfigurationManager.AppSettings["assembly"]);
                LibrayManagent adminTable = new LibrayManagent();
                var jsonData = get.GetEntitys(x => true, sc).ToList();
                //将对象序列化为JSON格式
                var json = CreateJson(jsonData);
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            return new HttpStatusCodeResult(404, "无法保存");
        }
    }
}