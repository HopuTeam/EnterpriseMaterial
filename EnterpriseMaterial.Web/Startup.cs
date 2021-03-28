using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EnterpriseMaterial.Web
{
    public class Startup
    {
        public IConfiguration Configuration;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                // options.Filters.Add<Filters.AuthFilter>();
                options.EnableEndpointRouting = false;
            });
            services.AddDbContext<Data.CoreEntities>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("EFDbConnection"), new MySqlServerVersion(new Version(5, 6, 50)));
            });
            services.AddSession();
            //BLL²ã½Ó¿Ú×¢Èë
            services.AddScoped<ILogic.lGoodsBLL, Logic.GoodsBLL>();
            services.AddScoped<ILogic.IBorrowBLL, Logic.BorrowBLL>();
            services.AddScoped<ILogic.ITypeLogic, Logic.TypeLogic>();
            services.AddScoped<ILogic.ICategoryBLL, Logic.CategoryBLL>();
            services.AddScoped<ILogic.IIdentityBLL, Logic.IdentityBLL>();
            services.AddScoped<ILogic.IUserLogic, Logic.UserLogic>();
            services.AddScoped<ILogic.ISignLogic, Logic.SignLogic>();
            services.AddScoped<ILogic.IDepartmentService, Logic.DepartmentService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseSession();
            app.UseStaticFiles();
            app.UseSession();

            app.UseMvc(options =>
            {
                options.MapRoute("Default", "{Controller=Sign}/{Action=Index}/{ID?}");
            });
        }
    }
}