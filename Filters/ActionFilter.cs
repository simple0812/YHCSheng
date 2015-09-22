using Microsoft.AspNet.Mvc;
using System;

namespace YHCSheng.Filters {
    public class ActionFilter : IActionFilter {
        public void OnActionExecuting(ActionExecutingContext context) {
            var typeName = context.Controller.GetType().FullName;
            Console.WriteLine(typeName + "." + context.ActionDescriptor.Name + ":Start");
        }

        public void OnActionExecuted(ActionExecutedContext context) {
            var typeName = context.Controller.GetType().FullName;
            Console.WriteLine(typeName + "." + context.ActionDescriptor.Name + ":END");
        }
    }
}