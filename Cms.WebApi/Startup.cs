using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Cms.WebApi.Auth;
using Cms.WebApi.AuthContext;
using Core.DbContext;
using Core.Repository;
using Core.Repository.Imp;
using Core.Service;
using Core.Service.Adapter;
using Core.Service.Imp;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;


namespace Cms.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IContainer ApplicationContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o =>
               o.AddPolicy("*", 
               cpBuilder => cpBuilder
                       .AllowAnyHeader()
                       .AllowAnyMethod()
                       .AllowAnyOrigin()
                       .AllowCredentials()
            ));

            services.AddHttpContextAccessor();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppAuthenticationSettings>(appSettingsSection);
            // JWT
            var appSettings = appSettingsSection.Get<AppAuthenticationSettings>();
            services.AddJwtBearerAuthentication(appSettings);
            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
            services.AddDbContext<CMSDbContext>(option => option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //services.AddScoped<IUserService, UserService>();
            //services.AddScoped<IUserRepository, UserRepository>();

            Mappings.RegisterMappings();

            services.AddAutofac();

            #region AutoFac注入

            var builder = new ContainerBuilder();
            //InstancePerLifetimeScope：同一个Lifetime生成的对象是同一个实例
            //SingleInstance：单例模式，每次调用，都会使用同一个实例化的对象；每次都用同一个对象；
            //InstancePerDependency：默认模式，每次调用，都会重新实例化对象；每次请求都创建一个新的对象；

            //获取所有需要依赖注入的程序集
            //Core.Repository，Core.Service是服务所在程序集命名空间  
            Assembly assembliesDomain = Assembly.Load("Core.Repository");
            Assembly assembliesService = Assembly.Load("Core.Service");
            //自动注册接口
            builder.RegisterAssemblyTypes(assembliesDomain)
                .AsImplementedInterfaces()
                .InstancePerDependency(); //见上方说明
            builder.RegisterAssemblyTypes(assembliesService)
             .AsImplementedInterfaces()
             .InstancePerDependency(); //见上方说明

            builder.Populate(services);
            this.ApplicationContainer = builder.Build();
            #endregion
            return new AutofacServiceProvider(this.ApplicationContainer);
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var serviceProvider = app.ApplicationServices;
            var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
            AuthContextService.Configure(httpContextAccessor);

            app.UseStaticFiles();
            app.UseFileServer();
            app.UseAuthentication();
            app.UseCors("*");
            app.UseMvc();
        }
    }
}
