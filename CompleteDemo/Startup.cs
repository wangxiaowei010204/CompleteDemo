using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using CompleteDemo.DataBase;
using CompleteDemo.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NLog.Extensions.Logging;
using NLog.Web;
using Swashbuckle.AspNetCore.Swagger;
using AutoMapper;
using CompleteDemo.Common;

namespace CompleteDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private static IContainer Container { get; set; }

        /// <summary>
        /// 配置依赖注入（DI）
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var builder = new ContainerBuilder();

            services.AddMemoryCache();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddCors(options =>
            {
                options.AddPolicy("any", x =>
                {
                    x.AllowAnyOrigin() //允许任何来源的主机访问
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();//指定处理cookie
                });
            });

            //注册Swagger生成器，定义一个和多个Swagger 文档
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("V1", new Info
                {
                    Version = "V1",
                    Title = "wxw's API",
                    Description = "A simple example ASP.NET Core Web API",
                    TermsOfService = "None",
                    Contact = new Contact
                    {
                        Name = "汪晓伟",
                        Email = string.Empty,
                        Url = "https://www.baidu.com"
                    },
                    License = new License
                    {
                        Name = "许可证名字",
                        Url = "https://www.baidu.com"
                    }
                });
                // 为 Swagger JSON and UI设置xml文档注释路径
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//获取应用程序所在目录
                var xmlPath = Path.Combine(basePath, "CompleteDemo.xml");
                c.IncludeXmlComments(xmlPath);
            });

            //添加对AutoMapper的支持
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<AutoMapperConfig>();
            }, AppDomain.CurrentDomain.GetAssemblies());

            builder.Populate(services);
            builder.RegisterType<LogInterceptor>();
            //注册程序集下所有类型
            //接口
            builder.RegisterAssemblyTypes(typeof(Startup).Assembly).AsImplementedInterfaces().EnableInterfaceInterceptors().InterceptedBy(typeof(LogInterceptor));

            //自身
            builder.RegisterAssemblyTypes(typeof(Startup).Assembly).AsSelf();

            var Container = builder.Build();

            return new AutofacServiceProvider(Container);
        }

        /// <summary>
        /// 配置ASP.NET Core的中间件
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //使用NLog作为日志记录工具
            loggerFactory.AddNLog();

            //引入Nlog配置文件
            env.ConfigureNLog("nlog.config");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseMiddleware<ExceptionHandlerMiddleware>().UseMiddleware<ContentMiddleware>();
            app.UseHttpsRedirection();
            app.UseMvc();

            //启用中间件服务生成Swagger作为JSON终结点
            app.UseSwagger();
            //启用中间件服务对swagger-ui，指定Swagger JSON终结点
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/V1/swagger.json", "My API V1");
            });

            app.UseCors("any");   //全局的，将影响所有控制器

        }
    }
}
