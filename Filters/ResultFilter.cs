using Microsoft.AspNet.Mvc;
using System;

namespace YHCSheng.Filters {
    public class ResultFilter : IResultFilter {
        public void OnResultExecuted(ResultExecutedContext filterContext) {
            Console.WriteLine("Result已经执行了!");
        }

        public void OnResultExecuting(ResultExecutingContext filterContext) {
            Console.WriteLine("Result执行之前!");
        }
    }
}