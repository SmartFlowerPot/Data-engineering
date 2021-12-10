using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using WebAPI.Gateway;
using WebAPI.Gateway.Service;
using WebAPI.Persistence;
using WebAPI.Persistence.Interface;
using WebAPI.Services;
using WebAPI.Services.Interface;

namespace WebAPI
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
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "WebAPI", Version = "v1"}); });
            
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAccountRepo, AccountRepo>();
            
            services.AddScoped<ITemperatureService, TemperatureService>();
            services.AddScoped<ITemperatureRepo, TemperatureRepo>();
            
            services.AddScoped<IPlantService, PlantService>();
            services.AddScoped<IPlantRepo, PlantRepo>();
            
            services.AddScoped<IHumidityService, HumidityService>();
            services.AddScoped<IHumidityRepo, HumidityRepo>();
            
            services.AddScoped<ICO2Service, CO2Service>();
            services.AddScoped<ICO2Repo, CO2Repo>();

            services.AddScoped<ILoriotService, LoriotService>();

            services.AddScoped<IMeasurementService, MeasurementService>();
            services.AddScoped<IMeasurementRepo, MeasurementRepo>();

            services.AddScoped<IWindowService, WindowService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1"));
            }

            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1"));
            
            app.UseHttpsRedirection();
            
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}