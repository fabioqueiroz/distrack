using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DisTrack.Constants;
using DisTrack.Data.Access;
using DisTrack.Data.Access.RepositoryInterfaces;
using DisTrack.Domain.Interfaces;
using DisTrack.Domain.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Rubrics.Data.Access;
using Rubrics.Data.Access.ConnectionFactory;


namespace DisTrackProject
{
    public class Startup
    {
        public const string AppS3BucketKey = "AppS3Bucket";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            // Dapper connection setup
            var connectionString = new ConnectionString(Configuration.GetConnectionString("DefaultConnection"));
            services.AddSingleton(connectionString);

            // Swagger setup
            services.AddSwaggerGen();

            // CORS policy
            services.AddCors(options =>
            {
                options.AddPolicy(SystemConstants.Cors.AllowAllPolicy,
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3000")
                                            .AllowAnyHeader()
                                            .AllowAnyMethod();
                    });

                //options.AddDefaultPolicy(
                //    builder =>
                //    {
                //        builder.WithOrigins("http://localhost:3000");

                //    });
            });

            services
                .AddScoped<IUserService, UserService>()
                .AddScoped<ITripService, TripService>()
                .AddScoped<IDisTrackRepository, DisTrackRepository>();

            services.AddControllers().AddNewtonsoftJson();

            services.AddDbContext<Context>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"), opts => opts.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds)));
          

            // Add S3 to the ASP.NET Core dependency injection framework.
            services.AddAWSService<Amazon.S3.IAmazonS3>();           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseRouting();

            //app.UseCors();
            app.UseCors(SystemConstants.Cors.AllowAllPolicy);
         
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwaggerUI();
        }
    }
}
