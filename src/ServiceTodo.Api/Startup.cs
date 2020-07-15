using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RestSharp;
using ServiceTodo.Api.Models.Validation;
using ServiceTodo.Api.Services;
using ServiceTodo.Domain.Interfaces;
using ServiceTodo.Integration.Repository;
using ServiceTodo.Api.Middlewares;
using Microsoft.OpenApi.Models;

namespace ServiceTodo.Api
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
            services
                .AddControllers()
                .AddFluentValidation(fv =>
                {
                    fv.RegisterValidatorsFromAssemblyContaining<GetTodosRequestValidator>();
                    fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                }); ;

            services.AddScoped<IRestClient, RestClient>();
            services.AddScoped<ITodoRepository, TodoRepository>();
            services.AddScoped<ITodoService, TodoService>();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "service-todo",
                    Version = "v1",
                    Description = "Simple .NET Core REST API to fetch todos using pagination",
                    Contact = new OpenApiContact
                    {
                        Name = "Aleksandar Stojanoski",
                        Url = new Uri("https://aleksandarstojanoski.now.sh")
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCustomErrorHandler();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "service-todo");
            });
        }
    }
}
