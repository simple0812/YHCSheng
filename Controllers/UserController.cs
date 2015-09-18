using System;
using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.DependencyInjection;
using System.Diagnostics;
using System.Collections.Generic;

using YHCSheng.Dal;
using YHCSheng.Utils;
using YHCSheng.Models;
using YHCSheng.Test;
using YHCSheng.Bll;


namespace YHCSheng.Controllers {
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
	}	
}
