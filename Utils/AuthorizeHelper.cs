using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace YHCSheng.Utils {
    public class AuthorizeHelper {
        public static bool Authorize(Type type, string methodName) {
            MethodInfo method = type.GetMethod(methodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            return Authorize(method);
        }

        public static bool Authorize(MethodInfo method) {
            if (method == null) method = (MethodInfo)(new StackTrace().GetFrame(1).GetMethod());
            bool ret = false;
            string currRole = "";
            string name = "";
            
            //var token = method.GetCustomAttribute(typeof(CustomResAttribute), false) as CustomResAttribute;


            return ret;
        }

    }
}
