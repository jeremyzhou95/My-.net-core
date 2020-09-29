using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OptionDemo.Service;

namespace OptionDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOrderService(Configuration.GetSection("OrderService"));         //使用拓展方法进行注册，不同的模块使用不同的拓展方法

            services.PostConfigure<OrderServiceOption>(options =>           //动态配置注册对象
            {
                options.MaxOrderCount = +100;
            });

            //services.AddControllers();
            //services.Configure<OrderServiceOption>(Configuration.GetSection("OrderService"));       //通过Configuration注册，单例模式
            //                                                                                        //services.AddSingleton<OrderServiceOption>();                   //注册配置文件单例
            //                                                                                        //services.AddSingleton<IOrderService,OrderService>();        //注册接口(单例)
            ////services.AddTransient<IOrderService, OrderService>();        //注册接口(瞬时模式：每次请求，都获取一个新的实例。即使同一个请求获取多次也会是不同的实例)
            //services.AddScoped<IOrderService, OrderService>();        //注册接口(每次请求，都获取一个新的实例。同一个请求获取多次会得到相同的实例)
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
