using BuildingBlocks.EventBusKafka.Consumer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Consumer.Infrastructure.Repositories;
using Wallet.Consumer.Models;

namespace Wallet.Consumer.Handlers
{
    public class BankTransactionCreatedHandler : IKafkaHandler<string, Transaction>
    {
        private readonly IWalletRepository _walletRepository;

        public BankTransactionCreatedHandler(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public async Task HandleAsync(string key, Transaction value)
        {
            var wallet = new WalletModel
            {
                DateOfTransaction = value.DateOfTransaction,
                Description = value.Description,
                Origin = EnumOrigin.BANK,
                TypeTransaction = value.TypeTransaction,
                Value = value.Value
            };

            await _walletRepository.AddAsync(wallet);
        }
    }
}
