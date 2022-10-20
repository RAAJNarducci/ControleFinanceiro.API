using BankTransactions.API.Commands;
using BankTransactions.API.Infrastructure.Repositories;
using BankTransactions.API.Model;
using BuildingBlocks.EventBusKafka;
using Hangfire;
using Logs.Services;
using MediatR;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BankTransactions.API.CommandHandlers
{
    public class TransactionCommandHandler
        : IRequestHandler<NewTransactionCommand, bool>,
          IRequestHandler<FindAllTransactionCommand, IEnumerable<Transaction>>,
          IRequestHandler<FindTransactionCommand, Transaction>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IKafkaMessageBus<string, Transaction> _bus;
        private readonly ILoggerService _loggerService;

        public TransactionCommandHandler(ITransactionRepository transactionRepository, IKafkaMessageBus<string, Transaction> bus, ILoggerService loggerService)
        {
            _transactionRepository = transactionRepository;
            _bus = bus;
            _loggerService = loggerService;
        }

        public async Task<bool> Handle(NewTransactionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Transaction transaction = request.Transaction;
                transaction.Id = ObjectId.GenerateNewId().ToString();
                await _transactionRepository.AddTransactionAsync(transaction);

                await _loggerService.LogInformation(new Logs.LoggerRequest
                {
                    ApplicationName = "Transactions.API",
                    Message = "Transação adicionada",
                });

                await _bus.PublishAsync(request.Transaction.Id, transaction);

                await _loggerService.LogInformation(new Logs.LoggerRequest
                {
                    ApplicationName = "Transactions.API",
                    Message = $"Evento da transação {transaction.Id} publicada",
                });

                return true;
            }
            catch (Exception ex)
            {
                await _loggerService.LogInformation(new Logs.LoggerRequest
                {
                    ApplicationName = "Transactions.API",
                    Message = $"Erro {ex.Message}, agendado no hangfire",
                });

                var jobId = BackgroundJob.Schedule(
                    () => Handle(request, cancellationToken),
                    TimeSpan.FromSeconds(30));

                return false;
            }
        }

        public async Task<IEnumerable<Transaction>> Handle(FindAllTransactionCommand request, CancellationToken cancellationToken)
        {
            return await _transactionRepository.GetTransactionsAsync();
        }

        public async Task<Transaction> Handle(FindTransactionCommand request, CancellationToken cancellationToken)
        {
            return await _transactionRepository.GetTransactionAsync(request.Id);
        }
    }
}
