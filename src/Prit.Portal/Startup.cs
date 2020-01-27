using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Prit.Infra.Cross;
using Prit.Infra.Data;
using Prit.Portal.Identity;

namespace Prit.Portal
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public const string CookieScheme = "PritScheme";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<LoginContext>(options =>
              options.UseSqlServer(
                  Configuration.GetConnectionString("Prit"),
                  sqlServerOptions => sqlServerOptions.EnableRetryOnFailure(3)));

            services.AddDbContext<PritContext>(options =>
              options.UseSqlServer(
                  Configuration.GetConnectionString("Prit"),
                  sqlServerOptions => sqlServerOptions.EnableRetryOnFailure(3)));

            services.AddIdentity<PritPortalUser, IdentityRole>()
            .AddEntityFrameworkStores<LoginContext>()
            .AddDefaultTokenProviders();

            services.AddAuthentication();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Auth/Index";
                options.LogoutPath = $"/Auth/Logout";
                options.AccessDeniedPath = $"/Auth/AccessDenied";
                options.SlidingExpiration = true;
            });

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;
            });

            Bootstrap.RegisterAllServices(services);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);            
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseStaticFiles();
       
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();

            UpdateDatabase(app);
        }
        private static void UpdateDatabase(IApplicationBuilder app)
        {
            //Wait for sqlserver
            if(!Debugger.IsAttached)
                Thread.Sleep(15000);

            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<LoginContext>())
                {
                    context.Database.Migrate();
                }

                using (var context = serviceScope.ServiceProvider.GetService<PritContext>())
                {
                    context.Database.Migrate();
                }
            }
        }

    }
}
