using BuildingBlocks.EventBusKafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Wallet.Consumer.Handlers;
using Wallet.Consumer.Infrastructure;
using Wallet.Consumer.Models;

namespace Wallet.Consumer
{
    class Program
    {
        public static void Main(string[] args)
        {
            IConfiguration Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();

            AddServices(Configuration);
        }

        private static void AddServices(IConfiguration Configuration)
        {
            var serviceProvider = new ServiceCollection();
            var settings = new WalletSettings
            {
                ConnectionString = Configuration.GetSection("ConnectionString").Value,
                Database = Configuration.GetSection("Database").Value
            };


            void showMethod(WalletSettings WalletSettings) => GetSettings(Configuration);
            serviceProvider.Configure((Action<WalletSettings>)showMethod);
            serviceProvider.AddKafkaConsumer<string, Transaction, BankTransactionCreatedHandler>(p =>
            {
                p.Topic = "transactions";
                p.GroupId = "transactions_group";
                p.BootstrapServers = "localhost:9092";
            });
        }

        private static WalletSettings GetSettings(IConfiguration Configuration)
        {
            return new WalletSettings
            {
                ConnectionString = Configuration.GetSection("ConnectionString").Value,
                Database = Configuration.GetSection("Database").Value
            };
        }
    }

}
