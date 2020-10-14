using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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

        //����ʱ���ã�ִ����ִ��һ��

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region ע��Swagger����

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("V1", new OpenApiInfo
                {
                    Title="Test",
                    Version="01",
                    Description="WebApiѧϰ"
                });
            });

            #endregion

            services.AddControllers();
        }

        //����������ܵ�
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //ʹ���м��
            app.UseRouting();

            //ʹ�þ�̬�ļ� wwwroot�ļ���
            app.UseStaticFiles();

            app.UseAuthorization();

            //ʹ��Swagger�м��
            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/V1/swagger.json", "Test");
            });

            //ע���м��˳��
            app.Use(async (context, next) =>
            {
                Console.WriteLine("MidWare1 Start");
                await next();
                Console.WriteLine("MidWare1 End");
            });

            app.Use(async (context, next) =>
            {
                Console.WriteLine("MidWare2 Start");
                await next();
                Console.WriteLine("MidWare2 End");
            });
            app.Use(async (context, next) =>
            {
                Console.WriteLine("MidWare3 Start");
                await next();
                Console.WriteLine("MidWare3 End");
            });
            app.Use(async (context, next) =>
            {
                Console.WriteLine("MidWare4 Start");
                await next();
                Console.WriteLine("MidWare4 End");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
