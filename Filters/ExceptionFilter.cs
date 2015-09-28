using System;
using Microsoft.AspNet.Mvc;

namespace YHCSheng.Filters {
    public class ExceptionFilter : IExceptionFilter {
        public void OnException(ExceptionContext filterContext) {
            var controller = filterContext.RouteData.Values["controller"] as string;
            var action = filterContext.RouteData.Values["action"] as string;

            Console.WriteLine("{0}:{1}发生异常!{2}", controller, action, filterContext.Exception.StackTrace);
        }
    }
}