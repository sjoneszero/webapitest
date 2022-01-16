using CoraWebAPITest.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;


namespace CoraWebAPITest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // There will be many instances of this Web API.
        public void ConfigureServices(IServiceCollection services)
        {
            var project1 = new DTO.Project();
            project1.Name = "New IT System";
            project1.Name = DateTime.UtcNow.ToString();
            project1.Name = "Eastern Standard Time";

            var project2 = new DTO.Project();
            project2.Name = "Lease new hardware";
            project2.Name = DateTime.UtcNow.ToString();
            project2.Name = "Eastern Standard Time";
             
            var projects = new Dictionary<int, DTO.Project>
           {
                { 1, project1 }, { 2, project2 }
           };

            services.AddSingleton<IDictionary<int, DTO.Project>>(projects);
            services.AddSingleton<ITimezoneLookup>(new TimezoneLookup());
            services.AddSingleton<IProjectValidator>(new ProjectValidator()); 
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CoraWebAPITest", Version = "v1" });
            });
        }

        // This code has been changed and an issue was introduced.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CoraWebAPITest v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
