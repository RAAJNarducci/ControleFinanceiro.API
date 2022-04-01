using CreditCard.API.Infrastructure;
using CreditCard.API.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Linq;

namespace CreditCard.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CreditCard.API", Version = "v1" });
            });
            services.AddScoped<ICreditCardService, CreditCardService>();
            services.AddDbContext<CreditCardContext>(opt => opt.UseSqlServer(Configuration["ConnectionString"]));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CreditCard.API v1"));

            app.UseRouting();

            app.UseAuthorization();

            //Estratégia para Migrations automáticas quando existir pendentes
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<CreditCardContext>();
                try
                {
                    var pendingMigrations = context.Database.GetPendingMigrations();
                    if (pendingMigrations.Any())
                    {
                        context.Database.Migrate();
                        Console.WriteLine($"Migration Apply with succeded");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error Migration: {ex.Message}");
                }
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
