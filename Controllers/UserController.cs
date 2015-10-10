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
using YHCSheng.Extensions;
using YHCSheng.Models;
using YHCSheng.Utils;

namespace YHCSheng.Controllers {
    public class UserController : Controller {
        private readonly ServiceBase<User> _service;

        public UserController() {
            var xbuilder = new ContainerBuilder();
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

            if (Context == null) return CustomJsonResult.Instance.GetSuccess(user);
            Context.Session.Set<User>("user" + user.Id, user);
            Console.WriteLine(Context.Session.Get<User>("user" + user.Id).Name);

            return CustomJsonResult.Instance.GetSuccess(user);
        }

        public object List() {
            int pageSize, pageIndex;
            pageSize = int.TryParse(Request.Query.Get("pageSize"), out pageSize) ? pageSize : 1;
            pageIndex = int.TryParse(Request.Query.Get("pageIndex"), out pageIndex) ? pageIndex : 10;
            var recordCount = 0;
            var order = new Dictionary<string, bool> {{"Id", true}};

            var users = _service.GetPageList(pageSize, pageIndex,out recordCount, null, order).ToList();
            return CustomJsonResult.Instance.PageSuccess(users, recordCount);
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