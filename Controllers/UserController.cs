using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Autofac;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using YHCSheng.Bll;
using YHCSheng.Dal;
using YHCSheng.Models;
using YHCSheng.Test;
using YHCSheng.Utils;

namespace YHCSheng.Controllers {
    public class UserController : Controller {
        private ServiceBase<User> _service;

        public UserController() {
            var xbuilder = new ContainerBuilder();
            xbuilder.RegisterType<ApplicationDbContext>().As<DbContext>();
            xbuilder.RegisterType<EFClient<User>>().As<IDao<User>>();
            xbuilder.RegisterType<UserService>();
            using (var container = xbuilder.Build()) {
                _service = container.Resolve<UserService>();
            }
        }

        public string Add() {
            var reader = new StreamReader(Request.Body);
            var txt = reader.ReadToEnd();
            var p = JsonConvert.DeserializeObject<User>(txt);
            var user = _service.Create(p);

            return CustomJsonResult.Instance.GetSuccess(user);
        }

        public object List() {
            int id;
            id = int.TryParse(Request.Query.Get("id"), out id) ? id : 0;
            var name = Request.Query.Get("name");
            var conditions = new Dictionary<string, bool> {{"Id", true}, {"Name", true}};

            var users = _service.GetByCondition((x) => true, conditions).ToList();
            return CustomJsonResult.Instance.PageSuccess(users);
        }

        public string Update() {
            var reader = new StreamReader(Request.Body);
            var txt = reader.ReadToEnd();
            var user = JsonConvert.DeserializeObject<User>(txt);

            if (user != null && user.Id > 0) {
                _service.Update(user);
            }

            return CustomJsonResult.Instance.GetSuccess(user);
        }

        public string Remove(int id) {
            var reader = new StreamReader(Request.Body);
            var txt = reader.ReadToEnd();
            Console.WriteLine(txt);
            return "delete success";
        }

        public IActionResult Index() {
            return View();
        }

        public object UploadPortrait(IList<IFormFile> portraits) {
            if (portraits.Count == 0) {
                return CustomJsonResult.Instance.GetError("请选择需要上传的文件");
            }
            var fileName = "";

            foreach (var file in portraits) {
                fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                var supportedTypes = new[] {"jpg", "jpeg", "png", "gif", "bmp"};
                var fileExt = Path.GetExtension(fileName).Substring(1);
                if (!supportedTypes.Contains(fileExt)) {
                    return CustomJsonResult.Instance.GetError("file type error");
                }

                if (file.Length > 1024*1000*10) {
                    return CustomJsonResult.Instance.GetError("file size error");
                }

                var r = new Random();
                fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + r.Next(10000) + "." + fileExt;
                var filePath = Path.Combine(GlobalVariables.FilePath, fileName);
                file.SaveAs(filePath);
            }

            return CustomJsonResult.Instance.GetSuccess(GlobalVariables.FileServer + fileName);
        }
    }
}