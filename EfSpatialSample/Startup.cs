using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using EfSpatialSample.Models;
using EfSpatialSample.Commands;

namespace EfSpatialSample
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsEnvironment("Development"))
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();


        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("EfSpatialSample")));

            services.AddScoped<IDataContext>(provider => provider.GetService<DataContext>());
           

            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            services.AddMvc();

            //Seed(services.BuildServiceProvider().GetService<IDataContext>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApplicationInsightsRequestTelemetry();

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseMvc();
        }


        private void Seed(IDataContext context)
        {
            Console.WriteLine("Seeding...");

            if (context == null)
            {
                throw new NullReferenceException(nameof(IDataContext));
            }

            if (!context.PointsOfInterest.Any())
            {

                foreach (var item in Enumerable.Range(1, 10000))
                {
                    var point = new PointOfInterest
                    {
                        DateAdded = DateTime.UtcNow,
                        Latitude = 0,
                        Longitude = 0
                    };

                    var query = new AddPointOfInterest(context);

                    query.Execute(point);
                     Console.WriteLine("Adding POI");
                }

                context.SaveChanges();

            }
        }
    }
}