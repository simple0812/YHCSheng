using Microsoft.Framework.Configuration;
using Microsoft.AspNet.Hosting;
using System.IO;
using Microsoft.Extensions.PlatformAbstractions;

public static class GlobalVariables {
    public static IConfiguration Configuration {get; private set;}
    public static string DefaultConnectionString {get; private set;}
    public static string LogConnectionString {get; private set;}
    public static string ApplicationBasePath {get; private set;}
    public static string EnvironmentName {get; private set;}
    
    public static string FilePath => Path.Combine(ApplicationBasePath, "wwwroot/FilePath");
    public static string FileServer => "http://localhost:3000/FilePath/";
    
    public static void Init(IHostingEnvironment env, IApplicationEnvironment appEnv) {
        var builder = new ConfigurationBuilder()
            .AddJsonFile("config.json")
            .AddJsonFile($"config.{env.EnvironmentName}.json", optional: true);
        builder.AddEnvironmentVariables();

        GlobalVariables.Configuration = builder.Build();
        DefaultConnectionString = "server=.;uid=sa;pwd=123;database=YHCSheng;"; 
        LogConnectionString = "server=.;uid=sa;pwd=123;database=YHCShengLog;"; 
        //Configuration.Get(@"Data:DefaultConnection:ConnectionString");
        ApplicationBasePath = appEnv.ApplicationBasePath;
        EnvironmentName = env.EnvironmentName;
    } 
}