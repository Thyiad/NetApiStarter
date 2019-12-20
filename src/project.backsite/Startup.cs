using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using project.backsite.Filters;
using project.dal;

namespace project.backsite
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
            services.AddControllersWithViews(options=> {
                options.Filters.Add<MyActionFilterAttribute>();
                options.Filters.Add<MyExceptionFilterAttribute>();
            });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(cookieOptions =>
               {
                   cookieOptions.LoginPath = new PathString("/account/login");
                   cookieOptions.LogoutPath = new PathString("/account/logout");
                   cookieOptions.SlidingExpiration = true;
                   cookieOptions.ExpireTimeSpan = TimeSpan.FromHours(1);
               });
            services.AddAppServices();
            services.AddHttpClient();
            services.Configure<AppSettings>(Configuration);
            services.Configure<ConnectionStringsSection>(Configuration.GetSection("ConnectionStrings"));

            services.AddHttpContextAccessor();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            IOptions<AppSettings> settings, IOptions<ConnectionStringsSection> conn)
        {
            AppSettings.Instance = settings.Value;
            AppSettings.Instance.Environment = env;
            ConnectionStringsSection.Instance = conn.Value;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            //上传地址
            var uploadFullPath = Path.GetFullPath(AppSettings.Instance.Upload.UploadPath);
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(uploadFullPath),
                RequestPath = settings.Value.Upload.RequestPath,
            });

            app.UseRouting();

            app.UseCookiePolicy();
            app.UseAuthentication(); //开启验证
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
