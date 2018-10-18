using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebApi.Repository;
using WebApi.Services;
using WebApi.Services.Interfaces;
using WebApi.Model;
using WebApi.Data;
using WebApi.Models;

namespace WebApi
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
            services.AddSingleton<IExerciseService, ExerciseService>();
            services.AddSingleton<IScoreService, ScoreService>();
            services.AddSingleton<IExerciseRepository, ExerciseRepository>();
            services.AddSingleton<IScoreRepository, ScoreRepository>();
            services.AddSingleton<ILoggerFactory, LoggerFactory>();
            services.AddSingleton<IRepository<Exercise>, Repository<Exercise>>();
            services.AddSingleton<IRepository<Score>, Repository<Score>>();
            services.AddSingleton<IDataContext, ExerciseContext>();
            services.AddSingleton<IUnitOfWork, UnitOfWork>();

            services.AddMvc();
            // Add service and create Policy with options
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // global policy - assign here or on each controller
            app.UseCors("CorsPolicy");

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template: "api/{controller}/{action}/{id?}");

            //    routes.MapRoute(
            //        name: "default1",
            //        template: "{controller}/{action}/{id?}");

            //    routes.MapRoute(
            //        name: "exercise",
            //        template: "api/exercise/{id?}",
            //        defaults: new { controller = "Exercise", action = "GetExercise" });

            //    routes.MapRoute(
            //        name: "exercise1",
            //        template: "exercise/{id?}",
            //        defaults: new { controller = "Exercise", action = "GetExercise" });
            //});
        }
    }
}
