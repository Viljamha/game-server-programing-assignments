using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_api.Models;
using web_api.Repositories;
using web_api.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using web_api.Exceptions;
using web_api.Filters;

namespace web_api
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
            services.Configure<AuthenticationKey>(Configuration);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddTransient<PlayersProcessor>();
            services.AddTransient<ItemsProcessor>();
            services.AddTransient<LogsProcessor>();
            services.AddSingleton<IRepository, MongoDbRepository>();

            services.AddScoped<AuditActionFilter>();
            
            services.AddMvc(options =>
            {
                options.Filters.Add(new LevelFilterAttribute()); // an instance
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseMiddleware<AuthenticationMiddleware>();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                loggerFactory.AddConsole(Configuration.GetSection("Logging"));
                loggerFactory.AddDebug();
            }
            else
            {
                //app.UseHsts();
                app.UseExceptionHandler("/error");
            }
            app.UseStatusCodePages();
            app.UseMvc();
        }
    }
}
