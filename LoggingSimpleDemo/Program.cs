using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LoggingSimpleDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfigurationBuilder configBuilder = new ConfigurationBuilder();
            configBuilder.AddJsonFile("appsetting.json", optional: false, reloadOnChange: true);

            var config = configBuilder.Build();

            IServiceCollection servicesCollection = new ServiceCollection();
            servicesCollection.AddSingleton<IConfiguration>(p => config);  //工厂模式将配置对象注入容器，如果直接注入实例，则容器不会管理对象的生命周期

            servicesCollection.AddLogging(builder =>
            {
                builder.AddConfiguration(config.GetSection("Logging"));
                builder.AddConsole();
            });

            IServiceProvider service = servicesCollection.BuildServiceProvider();
            ILoggerFactory loggerFactory = service.GetServices<ILoggerFactory>().First();

            ILogger alogger = loggerFactory.CreateLogger("alogger");
            //alogger.LogDebug(2001,"qq");
            //alogger.LogInformation("hello");

            //var ex = new Exception("错误测试");
            //alogger.LogError(ex,ex.Message);

            Console.ReadKey();
        }
    }
}
