using Microsoft.Framework.Configuration;
using Microsoft.Framework.Configuration.Json;
using Microsoft.Framework.Runtime;
using Microsoft.AspNet.Hosting;
using System.IO;

public static class GlobalVariables {
    public static IConfiguration Configuration {get; private set;}
    public static string ConnectionString {get; private set;}
    public static string ApplicationBasePath {get; private set;}
    public static string EnvironmentName {get; private set;}
    
    public static string FilePath => Path.Combine(ApplicationBasePath, "wwwroot/FilePath");
    public static string FileServer => "http://localhost:3000/FilePath/";
    
    public static void Init(IHostingEnvironment env, IApplicationEnvironment appEnv) {
        var builder = new ConfigurationBuilder(appEnv.ApplicationBasePath)
            .AddJsonFile("config.json")
            .AddJsonFile($"config.{env.EnvironmentName}.json", optional: true);
        builder.AddEnvironmentVariables();

        GlobalVariables.Configuration = builder.Build();
        ConnectionString = Configuration.Get(@"Data:DefaultConnection:ConnectionString");
        ApplicationBasePath = appEnv.ApplicationBasePath;
        EnvironmentName = env.EnvironmentName;
    } 
}