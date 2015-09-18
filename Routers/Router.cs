//路由
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Routing;
using YHCSheng.Controllers;

namespace YHCSheng.Routers
{
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
            
            opt.Get("demo/{name}", c => c.Action<UserController>(x => x.Demo(Param<string>.Any)));
            opt.Get("api/user", c => c.Action<UserController>(x => x.List()));
            opt.Post("api/user", c => c.Action<UserController>(x => x.Add()));
            opt.Delete("api/user/{id}", c => c.Action<UserController>(x => x.Remove(Param<int>.Any)));
            opt.Put("api/user", c => c.Action<UserController>(x => x.Update()));
        }
	}
}