using Microsoft.AspNet.Mvc;
using System;

namespace YHCSheng.Filters {
    public class AuthorizationFilter : IAuthorizationFilter  {
        public void OnAuthorization(AuthorizationContext filterContext) {
            Console.WriteLine("执行authorization! 判断是否有权限。。。");
        }
    }
}