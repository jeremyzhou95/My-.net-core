using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using OptionDemo.Service;

namespace Microsoft.Extensions.DependencyInjection      //继承依赖注入的命名空间，使用拓展方法
{
    public static class OrderServiceExtensions
    {
        public static IServiceCollection AddOrderService(this IServiceCollection services, IConfiguration configuration)        //把配置作为参数传入
        {
            services.Configure<OrderServiceOption>(configuration);
            services.AddSingleton<IOrderService, OrderService>();
            return services;
        }
    }
}
