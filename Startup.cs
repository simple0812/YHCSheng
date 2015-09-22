using Autofac;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Runtime;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using YHCSheng.Dal;
using YHCSheng.Filters;
using YHCSheng.Routers;
using YHCSheng.Test;


public class Startup {
    public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv) {

        //依赖注入
        var xbuilder = new ContainerBuilder(); 
        xbuilder.RegisterType<LoggingService>(); 
        xbuilder.RegisterType<CustomerService>(); 
        using (var container = xbuilder.Build()) { 
            var customService = container.Resolve<CustomerService>(); 
        } 

        GlobalVariables.Init(env, appEnv);
    }

    public void Configure(IApplicationBuilder app) {
        app.UseStaticFiles();
        app.Use(next => new TimeRecorderMiddleware(next).Invoke);
        app.UseMvc(Router.Instance.Route);
    }

    public void ConfigureServices(IServiceCollection services) {
        services.AddScoped<ITodoRepository, TodoRepository>();
        services.AddSingleton<TestService>();

        services.AddMvc();

        services.Configure<MvcOptions>(Router.Instance.Route);

        services.Configure<MvcOptions>(options => {
            //options.Filters.Add(typeof(ActionFilter));
            //options.Filters.Add(typeof(ResultFilter));
            //options.Filters.Add(typeof(AuthorizationFilter));
            //options.Filters.Add(typeof(ExceptionFilter));
        });

        services.AddEntityFramework()
            .AddSqlServer()
            .AddDbContext<ApplicationDbContext>();
    }
}

public class TimeRecorderMiddleware {
    readonly RequestDelegate _next;

    public TimeRecorderMiddleware(RequestDelegate next)
    {
        if (next != null) _next = next;
    }

    public async Task Invoke(HttpContext context) {
        if (context == null) throw new ArgumentNullException(nameof(context));
        var sw = new Stopwatch();
        sw.Start();

        await _next(context);

        var msg = @"process time:{0} ms";
        Console.WriteLine(string.Format(msg, sw.ElapsedMilliseconds));
    }
}


