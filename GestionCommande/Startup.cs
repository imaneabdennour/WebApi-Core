using System;
using System.IO;
using System.Reflection;
using AutoMapper;
using GestionCommande.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;


namespace GestionCommande
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddScoped<ICommanderRepo, SqlCommanderRepo>();

            services.AddDbContext<CommanderContext>(opt => opt.UseSqlServer
                (Configuration.GetConnectionString("CommanderConnection"))
            );

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Swagger
            services.AddSwaggerGen(options => 
            {
                options.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Commands API",
                        Description = "Informations about my Commands API - Swagger",
                        Contact = new OpenApiContact
                        {
                           Name = "Imane Abdennour",
                           Email = "imane.abdennour0@gmail.com",
                        },                       
                        Version = "v1",
                       
                    });
                // Set the comments path for the Swagger JSON and UI.    
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

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

            app.UseHttpsRedirection();
            app.UseMvc();

            // Swagger :
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Commands API");
            });

        }
    }
}
