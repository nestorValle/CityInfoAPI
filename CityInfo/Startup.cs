using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.Abstractions;
using CityInfo.Entities;
using CityInfo.Mappers;
using CityInfo.Models;
using CityInfo.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using NLog.Extensions.Logging;

namespace CityInfo
{
    public class Startup
    {
        public static IConfiguration Configuaration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional:false,reloadOnChange:true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuaration = builder.Build();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddMvcOptions(o => 
                     o.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter())
                )
                //.AddJsonOptions(o=> {
                //    var castedResolver = o.SerializerSettings.ContractResolver as DefaultContractResolver;
                //    castedResolver.NamingStrategy = null;
                //})
                ;
#if DEBUG
            services.AddTransient<IMailServices, LocalMailService>();
#else
            services.AddTransient<IMailServices, CloudMailService>();
#endif
            string connectionString = Startup.Configuaration["ConnectionStrings:DefaultConnection"];
            services.AddDbContext<CityInfoContext>(o => o.UseSqlServer(connectionString));

            //add service injection
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IPointOfInterestService, PointOfInterestService>();

            //add mapper
            services.AddScoped<IMapper<City, CitityWithOutPointsOfInterestDto>, CityMapper>();
            services.AddScoped<IMapper<PointsOfInterest, PointOfInterestDto>, PointOfInterestMapper>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, CityInfoContext dbContext)
        {
            loggerFactory.AddConsole();

            loggerFactory.AddDebug();

            //loggerFactory.AddProvider(new NLog.Extensions.Logging.NLogLoggerProvider());
            loggerFactory.AddNLog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                //seed Data in dev Mode
                dbContext.SeedDataToCityInfoContext();
            }
            else {
                app.UseExceptionHandler();
            }
            //send the status code to the client web page
            app.UseStatusCodePages();

            //set automapper generic initialization
            AutoMapper.Mapper.Initialize(c => {
                c.CreateMap<CitityWithOutPointsOfInterestDto, City>();
                c.CreateMap<PointOfInterestDto, PointsOfInterest>();
            });

            app.UseMvc();
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
