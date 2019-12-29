using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Farm.Suppliers.Infrastructure;
using Farm.Suppliers.Infrastructure.AggregatesModel.SupplierAggregate;
using Farm.Suppliers.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Farm.Suppliers.WebAPI
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
            services.AddTransient<ISupplierRepository,SupplierRepository>();
            services.AddDbContext<SupplierContext>(options =>
            { 
                var dbServer = Environment.GetEnvironmentVariable("DB_SERVER") ?? "127.0.0.1";
                options.UseNpgsql($"Server={dbServer};Port=5432;Database=farm;User Id=postgres;Password=Michelin/1;");
            });
            AddSwagger(services);
            AddSwaggerGen(services);
            services.AddAutoMapper(typeof(Startup).Assembly);
        }

        private void AddSwaggerGen(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(description.GroupName, new OpenApiInfo()
                    {
                        Title = $"{typeof(Startup).Assembly.GetCustomAttribute<System.Reflection.AssemblyProductAttribute>().Product} {description.ApiVersion}",
                        Version = description.ApiVersion.ToString(),
                        Description = description.IsDeprecated ? $"{typeof(Startup).Assembly.GetCustomAttribute<AssemblyDescriptionAttribute>().Description} - DEPRECATED" : typeof(Startup).Assembly.GetCustomAttribute<AssemblyDescriptionAttribute>().Description
                    });
                }

                options.IncludeXmlComments(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), $"{typeof(Startup).Assembly.GetName().Name}.xml"));
            });
        }

        private void AddSwagger(IServiceCollection services)
        {
            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
            })
            .AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", $"{this.GetType().Assembly.GetCustomAttribute<System.Reflection.AssemblyProductAttribute>().Product} {description.GroupName.ToUpperInvariant()}");
                }
            });


            UpdateDatabase(app);
        }

        private void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<SupplierContext>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}
