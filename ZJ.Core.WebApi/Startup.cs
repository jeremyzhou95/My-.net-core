using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
    
namespace ZJ.Core.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        //运行时调用，执行且执行一次

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region 注册Swagger服务

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("V1", new OpenApiInfo
                {
                    Title="Test",
                    Version="01",
                    Description="WebApi学习"
                });
            });

            #endregion

            var symmetricKeyAsBase64 = "LaoZhouLaoZhouLaoZhouLaoZhou";
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidIssuer = "http://localhost:5000",//发行人
                ValidateAudience = true,
                ValidAudience = "http://localhost:5001",//订阅人
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromSeconds(30),
                RequireExpirationTime = true,
            };
             
            

            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddHttpContextAccessor();
            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();     //

            services.AddControllers();
        }

        //配置请求处理管道
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //使用中间件
            app.UseRouting();

            //使用静态文件 wwwroot文件夹
            app.UseStaticFiles();

            app.UseAuthorization();

            //使用Swagger中间件
            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/V1/swagger.json", "Test");
            });

            //注册中间件顺序
            //app.Use(async (context, next) =>
            //{
            //    Console.WriteLine("MidWare1 Start");
            //    await next();
            //    Console.WriteLine("MidWare1 End");
            //});

            //app.Use(async (context, next) =>
            //{
            //    Console.WriteLine("MidWare2 Start");
            //    await next();
            //    Console.WriteLine("MidWare2 End");
            //});
            //app.Use(async (context, next) =>
            //{
            //    Console.WriteLine("MidWare3 Start");
            //    await next();
            //    Console.WriteLine("MidWare3 End");
            //});
            //app.Use(async (context, next) =>
            //{
            //    Console.WriteLine("MidWare4 Start");
            //    await next();
            //    Console.WriteLine("MidWare4 End");
            //});
            app.UseAuthorization();
            app.UseMvc();

            //app.useendpoints(endpoints =>
            //{
            //    endpoints.mapcontrollers();
            //});
        }
    }
}
