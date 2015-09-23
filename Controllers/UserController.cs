using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.Net.Http.Headers;
using YHCSheng.Bll;
using YHCSheng.Models;
using YHCSheng.Utils;


namespace YHCSheng.Controllers
{
    public class UserController : Controller {

        public UserController() {
        }

		public string Demo(string name) {
            Console.WriteLine(this.Request.Query.Get("xx"));
			var users = new UserService().Retrieve().ToList();
            return JsonHelper.Instance.SerializeObject(users);
		}

        public string Add() {
            User user = new User() {
                Name = "zl",
                Nick = "xx"
            };

            user = new UserService().Create(user);
            return JsonHelper.Instance.SerializeObject(user);
        }

        public Object List() {
            int id = int.TryParse(this.Request.Query.Get("id"), out id) ? id : 0;
            string name = this.Request.Query.Get("name");
            Dictionary<string, object> conditions = new Dictionary<string, object>();


            if(id != 0) {
                conditions.Add("Id", id);
            }

            if(!string.IsNullOrEmpty(name)) {
                conditions.Add("Name", name);
            }

            var users = new UserService().Retrieve(conditions).ToList();
            return CustomJsonResult.Instance.PageSuccess(users);
        }

        public string Update() {
            int id = int.TryParse(this.Request.Query.Get("id"), out id) ? id : 0;
            string name = this.Request.Query.Get("name");
            User user = new UserService().GetById(id);

            if(user != null) {
                user.Name = name;
                new UserService().Update(user);
            }

            return JsonHelper.Instance.SerializeObject(user);
        }

        public string Remove(int id) {
            new UserService().Remove(id);
            return "delete success";
        }

	    public IActionResult Index() {
	        return View();
	    }

        public Object UploadPortrait(IList<IFormFile>  portraits) {
            if (portraits.Count == 0) {
                return CustomJsonResult.Instance.GetError("请选择需要上传的文件");
            }
            var fileName = "";

            foreach (var file in portraits) {
                fileName = ContentDispositionHeaderValue
                    .Parse(file.ContentDisposition)
                    .FileName.Trim('"');

                var supportedTypes = new[] { "jpg", "jpeg", "png", "gif","bmp" };
                var fileExt = Path.GetExtension(fileName).Substring(1);
                if (!supportedTypes.Contains(fileExt)) {
                    return CustomJsonResult.Instance.GetError("file type error");
                }
                
                if (file.Length > 1024 * 1000 * 10) {
                    return CustomJsonResult.Instance.GetError("file size error");
                }

                Random r = new Random();
                fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + r.Next(10000) + "." + fileExt;
                var filePath =Path.Combine(GlobalVariables.FilePath, fileName);
                file.SaveAs(filePath);
            }

            return CustomJsonResult.Instance.GetSuccess(GlobalVariables.FileServer +  fileName);
        }
	}	
}
