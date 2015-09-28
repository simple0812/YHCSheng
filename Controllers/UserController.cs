using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using YHCSheng.Bll;
using YHCSheng.Models;
using YHCSheng.Utils;


namespace YHCSheng.Controllers {
    public class UserController : Controller {

        public UserController() {}

        public string Demo(string name) {
            Console.WriteLine(this.Request.Query.Get("xx"));
            var users = new UserService().Retrieve().ToList();
            return JsonHelper.Instance.SerializeObject(users);
        }

        public string Add() {
            var reader = new StreamReader(this.Request.Body);
            var txt = reader.ReadToEnd();
            var p = JsonConvert.DeserializeObject<User>(txt);
            var user = new UserService().Create(p);

            return CustomJsonResult.Instance.GetSuccess(user);
        }

        public object List() {
            int id = int.TryParse(this.Request.Query.Get("id"), out id) ? id : 0;
            var name = this.Request.Query.Get("name");
            var conditions = new Dictionary<string, object>();

            if (id != 0) {
                conditions.Add("Id", id);
            }

            if (!string.IsNullOrEmpty(name)) {
                conditions.Add("Name", name);
            }

            var users = new UserService().Retrieve(conditions).ToList();
            return CustomJsonResult.Instance.PageSuccess(users);
        }

        public string Update() {
            var reader = new StreamReader(this.Request.Body);
            var txt = reader.ReadToEnd();
            var user = JsonConvert.DeserializeObject<User>(txt);

            if (user != null && user.Id > 0) {
                new UserService().Update(user);
            }

            return CustomJsonResult.Instance.GetSuccess(user);
        }

        public string Remove(int id) {
            var reader = new StreamReader(this.Request.Body);
            var txt = reader.ReadToEnd();
            Console.WriteLine(txt);
            //new UserService().Remove(id);
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
                fileName = ContentDispositionHeaderValue
                    .Parse(file.ContentDisposition)
                    .FileName.Trim('"');

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
