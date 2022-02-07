using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.IRepository;
using Blog.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyBlog.IService;
using MyBlog.Service;
using SqlSugar.IOC;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.Swagger;

namespace MyBlog.JWT
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v12", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "My API666", Version = "v12" });
            });
            services.AddSqlSugar(new IocConfig()
            {
                //ConfigId="db01"  多租户用到
                ConnectionString = this.Configuration["SqlConn"],
                DbType = IocDbType.SqlServer,
                IsAutoCloseConnection = true//自动释放
            }); //多个库就传List<IocConfig>

            services.AddCustomerIOC();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //启用中间件服务生成Swagger作为JSON终结点
            app.UseSwagger();
            //启用中间件服务对swagger-ui，指定Swagger JSON终结点
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v12/swagger.json", "My API API666");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    public static class IOCExtend
    {
        public static IServiceCollection AddCustomerIOC(this IServiceCollection services)
        {
 
            services.AddScoped<IWriterInforRepository, WriterInforRepository>();
            services.AddScoped<IWriterInforService, WriterInforService>();

            return services;
        }
    }
}
