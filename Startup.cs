using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Session;
using YHCSheng.Dal;
using YHCSheng.Filters;
using YHCSheng.Routers;
using Microsoft.Extensions.PlatformAbstractions;


namespace YHCSheng
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
            GlobalVariables.Init(env, appEnv);
        }

        public IConfigurationRoot Configuration { get; set; }

        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();
            app.Use(next => new TimeRecorderMiddleware(next).Invoke);
            app.UseSession();
            app.UseMvc(Router.Instance.Route);
        }

        public void ConfigureServices(Microsoft.Extensions.DependencyInjection.IServiceCollection services)
        {
            services.Configure<SessionOptions>(option => option.IdleTimeout = TimeSpan.FromMinutes(30));
            services.AddCaching();
            services.AddSession();

            services.AddMvc();

            services.Configure<MvcOptions>(Router.Instance.Route);

            services.Configure<MvcOptions>(options =>
            {
                //options.Filters.Add(typeof(ActionFilter));
                //options.Filters.Add(typeof(ResultFilter));
                //options.Filters.Add(typeof(AuthorizationFilter));
                options.Filters.Add(typeof(ExceptionFilter));
            });

            services.AddEntityFramework().AddSqlServer().AddDbContext<ApplicationDbContext>();
        }
    }

    public class TimeRecorderMiddleware
    {
        private readonly RequestDelegate _next;

        public TimeRecorderMiddleware(RequestDelegate next)
        {
            if (next != null) _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            var sw = new Stopwatch();
            sw.Start();

            await _next(context);

            var msg = @"{1} url:{0}  {2} ms";

            Console.WriteLine(msg, context.Request.Path, context.Request.Method, sw.ElapsedMilliseconds);
        }
    }
}