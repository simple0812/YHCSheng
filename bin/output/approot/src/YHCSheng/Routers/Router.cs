//路由
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Routing;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.ApplicationModels;

using YHCSheng.Controllers;

namespace YHCSheng.Routers {
	public sealed class Router {
		public static readonly Router Instance = new Router(); 
	    private Router() {}

        //普通路由配置 一般用于view路由
        public void Route(IRouteBuilder routeBuilder) {
            routeBuilder.MapRoute(name : "default", template : "{controller=User}/{action=Index}/{id?}");
        }

        //使用lamda配置路由 一般用于api路由
        public void Route(MvcOptions opt) {
            opt.EnableTypedRouting();
            opt.GetRoute("demo/{name}", c => c.Action<UserController>(x => x.Demo(Param<string>.Any)));
        }
	}
}