using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MongoDB.GenericRepository.Context;
using MongoDB.GenericRepository.Interfaces;
using MongoDB.GenericRepository.Persistence;
using MongoDB.GenericRepository.Repository;
using MongoDB.GenericRepository.UoW;
using System;

namespace MongoDB.GenericRepository
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins, builder =>
                {
                    builder.WithOrigins("http://localhost:3000");
                });
            });

            services.AddControllers()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddJsonOptions(opt =>
                {
                    var serializerOptions = opt.JsonSerializerOptions;
                    serializerOptions.IgnoreNullValues = true;
                    serializerOptions.IgnoreReadOnlyProperties = false;
                    serializerOptions.WriteIndented = true;
                });

            // Configure the persistence in another layer
            MongoDbPersistence.Configure();

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "LGApi",
                    Description = "Swagger surface",
                    Contact = new OpenApiContact
                    {
                        Name = "Caio Victor da Silva Viga",
                        Email = "vitor.da.alvorada@gmail.com",
                        Url = new Uri("https://ivega123.github.io")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT",
                        Url = new Uri("https://github.com/iVega123/LGApi/blob/main/LICENSE")
                    }
                });
            });

            RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger(c =>
            {
                c.RouteTemplate = "swagger/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "LG API v1.0");
            });
        }

        private void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IMongoContext, MongoContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
        }
    }
}