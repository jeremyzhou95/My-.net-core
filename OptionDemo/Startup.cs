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
            services.AddOrderService(Configuration.GetSection("OrderService"));         //ʹ����չ��������ע�ᣬ��ͬ��ģ��ʹ�ò�ͬ����չ����

            services.PostConfigure<OrderServiceOption>(options =>           //��̬����ע�����
            {
                options.MaxOrderCount = +100;
            });

            //services.AddControllers();
            //services.Configure<OrderServiceOption>(Configuration.GetSection("OrderService"));       //ͨ��Configurationע�ᣬ����ģʽ
            //                                                                                        //services.AddSingleton<OrderServiceOption>();                   //ע�������ļ�����
            //                                                                                        //services.AddSingleton<IOrderService,OrderService>();        //ע��ӿ�(����)
            ////services.AddTransient<IOrderService, OrderService>();        //ע��ӿ�(˲ʱģʽ��ÿ�����󣬶���ȡһ���µ�ʵ������ʹͬһ�������ȡ���Ҳ���ǲ�ͬ��ʵ��)
            //services.AddScoped<IOrderService, OrderService>();        //ע��ӿ�(ÿ�����󣬶���ȡһ���µ�ʵ����ͬһ�������ȡ��λ�õ���ͬ��ʵ��)
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
