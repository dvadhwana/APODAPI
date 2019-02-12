using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NLog.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using MongoDB.Driver;
using azuretestapp.Service;
using azuretestapp.DataAccess;
using azuretestapp.Model;

namespace azuretestapp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            AppSetting.LoadSetting();
        }

        private IServiceProvider BuildLogDI(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            //Creating logger factory to be used inside services.
            var logfactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            logfactory.AddNLog(new NLogProviderOptions { CaptureMessageTemplates = true, CaptureMessageProperties = true });
            NLog.LogManager.LoadConfiguration("nlog.config");
            return serviceProvider;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Azure Test API",
                    Description = "Documentation for Azure Test API",
                    TermsOfService = "None",
                    Contact = new Contact() { Name = "Divyesh Vadhwwana", Email = "divyeshv@cybage.com", Url = "www.cybage.com" }
                });
            });

            var serviceProvider = BuildLogDI(services);
            services.AddTransient(s => new MongoDBRepository(serviceProvider.GetRequiredService<ILogger<MongoDBRepository>>()));
            services.AddTransient(s => new APODService(serviceProvider.GetRequiredService<ILogger<APODService>>()));
            services.AddCors(o => o.AddPolicy("AllowAll", builder => { builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin(); }));
                
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Migration Rules API");
            });
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
