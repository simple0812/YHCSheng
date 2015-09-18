using System;
using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.DependencyInjection;
using System.Diagnostics;

using YHCSheng.Dal;
using YHCSheng.Utils;
using YHCSheng.Models;
using YHCSheng.Test;
using YHCSheng.Bll;


namespace YHCSheng.Controllers {
	public class UserController : Controller {
        private readonly ApplicationDbContext dbContext;

        public UserController() {
            //dbContext = new ApplicationDbContext();
        }

		public string Demo(string name) {
            Console.WriteLine(this.Request.Query.Get("xx"));
			var users = dbContext.Users.ToList();
            return JsonHelper.Instance.SerializeObject(users);
		}

        public string Add() {
            User user = new User() {
                Name = "zl",
                Nick = "xx"
            };

            dbContext.Set<User>().Add(user);
            dbContext.SaveChanges();
            return JsonHelper.Instance.SerializeObject(user);
        }

        public Object List() {
            var users = new UserService().GetList().ToList();
            return JsonHelper.Instance.SerializeObject(users);
        }

        public string Update(int id, string name) {
            User user = dbContext.Users.Where(p => p.Id ==id ).FirstOrDefault();

            if(user != null) {
                user.Name = name;
            }

            return JsonHelper.Instance.SerializeObject(user);
        }

        public string Remove(int id) {
            User user = dbContext.Users.Where(p => p.Id ==id ).FirstOrDefault();

            if(user == null) {
                return "user is not exists";
            }
            dbContext.Remove(user);
            dbContext.SaveChanges();

            return "delete success";
        }

	    public IActionResult Index() {
	        return View();
	    }
	}	
}
