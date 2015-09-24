using System;
using Microsoft.AspNet.Mvc;

namespace YHCSheng.Filters {
    public class ExceptionFilter : IExceptionFilter {
        public void OnException(ExceptionContext filterContext) {
            string controller = filterContext.RouteData.Values["controller"] as string;
            string action = filterContext.RouteData.Values["action"] as string;

            Console.WriteLine(string.Format("{0}:{1}发生异常!{2}", controller,action, filterContext.Exception.StackTrace));
        }
    }
}