using BankTransactions.API.Model;
using BuildingBlocks.EventBusKafka;
using Logs.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankTransactions.API.Services
{
    public class BankTransactionService
        : IBankTransactionService
    {
        private readonly IKafkaMessageBus<string, Transaction> _bus;
        private readonly ILoggerService _loggerService;

        public BankTransactionService(IKafkaMessageBus<string, Transaction> bus, ILoggerService loggerService)
        {
            _bus = bus;
            _loggerService = loggerService;
        }
        public async Task SyncTransaction(Transaction transaction)
        {
            try
            {
                var response = ConfirmTransactionAtBank();
                if (response.IdTransaction is not null)
                {
                    transaction.Id = response.IdTransaction;
                    await _bus.PublishAsync(response.IdTransaction, transaction);

                    await _loggerService.LogInformation(new Logs.LoggerRequest
                    {
                        ApplicationName = "Transactions.API",
                        Message = $"Evento da transação {transaction.Id} publicada",
                    });
                }
                if (response.StatusTransaction == StatusTransaction.NOT_FOUND)
                    throw new Exception("Transação não encontrada no Banco");
            }
            catch (Exception)
            {
                throw;
            }

        }

        public ItauTransaction ConfirmTransactionAtBank()
        {
            var rnd = new Random();
            var transactions = new List<ItauTransaction>()
            {
                new ItauTransaction
                {
                    IdTransaction = Guid.NewGuid().ToString(),
                    DateTransaction = new DateTime(2021-05-20),
                    StatusTransaction = StatusTransaction.CONFIRMED
                },
                new ItauTransaction
                {
                    StatusTransaction = StatusTransaction.NOT_FOUND
                },
                new ItauTransaction
                {
                    DateTransaction = new DateTime(2021-05-20),
                    StatusTransaction = StatusTransaction.WAITING
                }
            };
            int index = rnd.Next(transactions.Count);
            return transactions[index];
        }
    }
}
