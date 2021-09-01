using Autofac;
using Autofac.Extensions.DependencyInjection;
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
using Microsoft.Extensions.Options;
using ParkyAPI.Autofac;
using ParkyAPI.Data;
using ParkyAPI.ParkyMapper;
using ParkyAPI.Repository;
using ParkyAPI.Repository.IRepository;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;


namespace ParkyAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public ILifetimeScope AutofacContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Register DBContext
            services.AddDbContext<ApplicationDbContext>
                (options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //Register Automapper
            services.AddAutoMapper(typeof(ParkyMappings));
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
            });
            services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VVV");
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen();

            //services.AddSwaggerGen(options => 
            //{
            //    options.SwaggerDoc("ParkyOpenAPISpec", 
            //        new Microsoft.OpenApi.Models.OpenApiInfo()
            //        {
            //            Title = "Parky API (National Park)",
            //            Version = "1",
            //            Description = "Udemy Parky API NP",
            //            Contact = new Microsoft.OpenApi.Models.OpenApiContact()
            //            {
            //                Email = "mine@mine.com",
            //                Name = "Victor",
            //                Url = new Uri("https://www.google.com")
            //            },
            //            License = new Microsoft.OpenApi.Models.OpenApiLicense()
            //            {
            //                Name = " MIT License",
            //                Url = new Uri("Https://en.wikipedia.iorg/wiki/MIT_License")
            //            }
            //        });
            //    //options.SwaggerDoc("ParkyOpenAPISpecTrails",
            //    //    new Microsoft.OpenApi.Models.OpenApiInfo()
            //    //    {
            //    //        Title = "Parky API Trails",
            //    //        Version = "1",
            //    //        Description = "Udemy Parky API Trails",
            //    //        Contact = new Microsoft.OpenApi.Models.OpenApiContact()
            //    //        {
            //    //            Email = "mine@mine.com",
            //    //            Name = "Victor",
            //    //            Url = new Uri("https://www.google.com")
            //    //        },
            //    //        License = new Microsoft.OpenApi.Models.OpenApiLicense()
            //    //        {
            //    //            Name = " MIT License",
            //    //            Url = new Uri("Https://en.wikipedia.iorg/wiki/MIT_License")
            //    //        }
            //    //    });
            //    var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //    var cmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
            //    options.IncludeXmlComments(cmlCommentsFullPath);
            //});
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
            foreach (var desc in provider.ApiVersionDescriptions)
                options.SwaggerEndpoint($"/swagger/{desc.GroupName}/swagger.json",
                    desc.GroupName.ToUpperInvariant());
                options.RoutePrefix = "";
            });

            //app.UseSwaggerUI(options =>
            //{
            //    options.SwaggerEndpoint("/swagger/ParkyOpenAPISpec/swagger.json", "Parky API");
            //    //options.SwaggerEndpoint("/swagger/ParkyOpenAPISpecTrails/swagger.json", "Parky API Trails");
            //    options.RoutePrefix = "";
            //});

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Register your own things directly with Autofac here. Don't
            // call builder.Populate(), that happens in AutofacServiceProviderFactory
            // for you.
            builder.RegisterModule(new AutofacServiceModules());
        }

    }
}
