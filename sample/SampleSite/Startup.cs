﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SampleSite.Model;
using Microsoft.Extensions.Configuration;

namespace SampleSite
{
    public class Startup
    {

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddChaos<Page, Content, User, Role>(options => {
                // Password settings
                options.Security.Password.RequireDigit = true;
                options.Security.Password.RequiredLength = 8;
                options.Security.Password.RequireNonAlphanumeric = false;
                options.Security.Password.RequireUppercase = true;
                options.Security.Password.RequireLowercase = false;

                // Lockout settings
                options.Security.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Security.Lockout.MaxFailedAccessAttempts = 10;

                // Cookie settings
                options.Security.Cookies.ApplicationCookie.ExpireTimeSpan = TimeSpan.FromDays(150);
                options.Security.Cookies.ApplicationCookie.LoginPath = "/login";
                options.Security.Cookies.ApplicationCookie.LogoutPath = "/logout";

                // User settings
                options.Security.User.RequireUniqueEmail = false;
            })
                .AddJsonStores();
                //.AddEntityFrameworkStores<ApplicationDbContext, int>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //loggerFactory.AddConsole();
            //loggerFactory.AddDebug();
            app.UseChaos();
        }
    }
}
