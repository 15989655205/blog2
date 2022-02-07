using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.IRepository;
using Blog.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
using MyBlog.IService;
using MyBlog.Service;
using SqlSugar.IOC;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.Swagger;

namespace BlogCore
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
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "在下框中输入请求头中需要添加Jwt授权Token：Bearer Token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });
            services.AddSqlSugar(new IocConfig()
            {
                //ConfigId="db01"  多租户用到
                ConnectionString = this.Configuration["SqlConn"],
                DbType = IocDbType.SqlServer,
                IsAutoCloseConnection = true//自动释放
            }); //多个库就传List<IocConfig>

            
            services.AddCustomerIOC();
            services.AddCustomJWT();
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

            app.UseCookiePolicy();

            app.UseAuthentication();
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
            services.AddScoped<IBlogNewsRepository, BlogNewsRepository>();
            services.AddScoped<IBlogNewsService, BlogNewsService>();

            services.AddScoped<ITypeInfoRepository, TypeInfoRepository>();
            services.AddScoped<ITypeInfoService, TypeInfoService>();

            services.AddScoped<IWriterInforRepository, WriterInforRepository>();
            services.AddScoped<IWriterInforService, WriterInforService>();

            return services;
        }

        public static IServiceCollection AddCustomJWT(this IServiceCollection services)
        {
            string secret = "fc93cb07e1ad92898527100e58a1cf1d1e7f65e9a266a6f87f3c84feb541c7b3";
        //添加jwt验证：
                services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,//是否验证Issuer
                        ValidateAudience = true,//是否验证Audience
                        ValidateLifetime = true,//是否验证失效时间
                        ClockSkew = TimeSpan.FromSeconds(30),
                        ValidateIssuerSigningKey = true,//是否验证SecurityKey
                        ValidAudience = "http://localhost:5000",//Audience
                        ValidIssuer = "http://localhost:6060",//Issuer，这两项和前面签发jwt的设置一致
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret))//拿到SecurityKey
                    };
                });
            return services;
        }
    }
}
