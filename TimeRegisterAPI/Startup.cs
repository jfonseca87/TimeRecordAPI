using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TimeRegisterAPI.Business.Implementations;
using TimeRegisterAPI.Business.Interfaces;
using TimeRegisterAPI.Common;
using TimeRegisterAPI.Repository.Interfaces;
using TimeRegisterAPI.Repository.SQLImplementations;

namespace TimeRegisterAPI
{
    public class Startup
    {
        private readonly IConfiguration _conf;
        private readonly string corsConfigurationName = "TimeRegisterCors"; 

        public Startup(IConfiguration conf)
        {
            _conf = conf;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(config =>
            {
                config.AddPolicy(corsConfigurationName,
                builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });

            services.AddControllers();

            services.Configure<DatabaseOptions>(options => 
            {
                options.ConnectionString = _conf.GetConnectionString("SQLConnection");
            });

            services.AddTransient<ITimeRecordRepository, TimeRecordSQL>();
            services.AddTransient<ITimeRecordBusiness, TimeRecordBusiness>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(corsConfigurationName);

            app.UseRouting();

            app.UseEndpoints(endpoints => 
            {
                endpoints.MapControllers();
            });
        }
    }
}
