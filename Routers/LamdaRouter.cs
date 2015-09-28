using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace YHCSheng.Routers
{
    public class TypedRouteModel : AttributeRouteModel {
        public TypedRouteModel(string template) {
            Template = template;
            HttpMethods = new string[0];
        }

        public TypeInfo ControllerType { get; private set; }

        public MethodInfo ActionMember { get; private set; }

        public IEnumerable<string> HttpMethods { get; private set; }

        public TypedRouteModel Controller<TController>() {
            ControllerType = typeof(TController).GetTypeInfo();
            return this;
        }

        public TypedRouteModel Action<T, TU>(Expression<Func<T, TU>> expression) {
            ActionMember = GetMethodInfoInternal(expression);
            ControllerType = ActionMember.DeclaringType.GetTypeInfo();
            return this;
        }

        public TypedRouteModel Action<T>(Expression<Action<T>> expression) {
            ActionMember = GetMethodInfoInternal(expression);
            ControllerType = ActionMember.DeclaringType.GetTypeInfo();
            return this;
        }

        private static MethodInfo GetMethodInfoInternal(dynamic expression) {
            var method = expression.Body as MethodCallExpression;
            if (method != null)
                return method.Method;

            throw new ArgumentException("Expression is incorrect!");
        }

        public TypedRouteModel WithName(string name) {
            Name = name;
            return this;
        }

        public TypedRouteModel ForHttpMethods(params string[] methods) {
            HttpMethods = methods;
            return this;
        }
    }

    public class TypedRoutingApplicationModelConvention : IApplicationModelConvention {
        internal static readonly Dictionary<TypeInfo, List<TypedRouteModel>> Routes = new Dictionary<TypeInfo, List<TypedRouteModel>>();

        public void Apply(ApplicationModel application) {
            foreach (var controller in application.Controllers) {
                if (!Routes.ContainsKey(controller.ControllerType)) continue;
                var typedRoutes = Routes[controller.ControllerType];
                foreach (var route in typedRoutes) {
                    var action = controller.Actions.FirstOrDefault(x => x.ActionMethod == route.ActionMember);
                    if (action == null) continue;
                    action.AttributeRouteModel = route;
                    //注意这里是直接替换，会影响现有Controller上的Route特性定义的路由
                    foreach (var method in route.HttpMethods) {
                        action.HttpMethods.Add(method);
                    }
                }
            }
        }
    }

    public static class MvcOptionsExtensions {
        public static TypedRouteModel Get(this MvcOptions opts, string template, Action<TypedRouteModel> configSetup) {
            return AddRoute(template, configSetup).ForHttpMethods("GET");
        }

        public static TypedRouteModel Post(this MvcOptions opts, string template, Action<TypedRouteModel> configSetup) {
            return AddRoute(template, configSetup).ForHttpMethods("POST");
        }

        public static TypedRouteModel Put(this MvcOptions opts, string template, Action<TypedRouteModel> configSetup) {
            return AddRoute(template, configSetup).ForHttpMethods("PUT");
        }

        public static TypedRouteModel Delete(this MvcOptions opts, string template, Action<TypedRouteModel> configSetup) {
            return AddRoute(template, configSetup).ForHttpMethods("DELETE");
        }

        public static TypedRouteModel TypedRoute(this MvcOptions opts, string template, Action<TypedRouteModel> configSetup) {
            return AddRoute(template, configSetup);
        }

        private static TypedRouteModel AddRoute(string template, Action<TypedRouteModel> configSetup) {
            var route = new TypedRouteModel(template);
            configSetup(route);

            if (TypedRoutingApplicationModelConvention.Routes.ContainsKey(route.ControllerType)) {
                var controllerActions = TypedRoutingApplicationModelConvention.Routes[route.ControllerType];
                controllerActions.Add(route);
            } else {
                var controllerActions = new List<TypedRouteModel> { route };
                TypedRoutingApplicationModelConvention.Routes.Add(route.ControllerType, controllerActions);
            }

            return route;
        }

        public static void EnableTypedRouting(this MvcOptions opts) {
            opts.Conventions.Add(new TypedRoutingApplicationModelConvention());
        }
    }

    public static class Param<TValue> {
        public static TValue Any => default(TValue);
    }

}