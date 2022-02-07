using GraphQLApi.Controllers;
using GraphQLApi.Mutation;
using GraphQLApi.Queries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Model;
using StubLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TarotDB;
using TarotDB2Model;

namespace GraphQLApi
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
            services.AddScoped<DbContext, TarotContext>();
            services.AddSingleton<IDataManager, TarotDBManager>();
            services.AddAutoMapper(typeof(Startup));
            services.AddGraphQLServer().AddQueryType<GameQuery>();
            services.AddGraphQLServer().AddMutationType<GameMutation>();
            services.AddGraphQLServer().AddDefaultTransactionScopeHandler();
            services.AddDbContext<TarotContext>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseGraphQLPlayground();
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            using(var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using(var context = serviceScope.ServiceProvider.GetService<DbContext>())
                {
                    context.Database.Migrate();
                }
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });
        }
    }
}
