using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MediatR;
using BankTransactions.API.Infrastructure.Repositories;
using BuildingBlocks.EventBusKafka;
using BankTransactions.API.Model;
using Logs.Services;
using Hangfire;
using Hangfire.Mongo;
using Hangfire.Mongo.Migration.Strategies;
using Hangfire.Mongo.Migration.Strategies.Backup;

namespace BankTransactions.API
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
            services.Configure<TransactionSettings>(Configuration);
            services.AddTransient<ITransactionRepository, TransactionRepository>();
            services.AddTransient<ILoggerService, LoggerService>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BankTransactions.API", Version = "v1" });
            });

            var migrationOptions = new MongoMigrationOptions
            {
                MigrationStrategy = new MigrateMongoMigrationStrategy(),
                BackupStrategy = new CollectionMongoBackupStrategy()
            };

            services.AddMediatR(typeof(Startup));

            services.AddHangfire(x =>
                x.UseMongoStorage(Configuration.GetSection("ConnectionString").Value, "HangFire", new MongoStorageOptions { MigrationOptions = migrationOptions }));

            services.AddKafkaMessageBus();

            services.AddKafkaProducer<string, Transaction>(p =>
            {
                p.Topic = Configuration.GetSection("KafkaTopic").Value;
                p.BootstrapServers = Configuration.GetSection("KafkaServer").Value;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
               
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BankTransactions.API v1"));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseHangfireDashboard();
            
            app.UseHangfireServer();
        }
    }
}
