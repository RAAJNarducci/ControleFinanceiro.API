using BuildingBlocks.EventBusKafka.Consumer;
using System.Threading.Tasks;
using Wallet.API.Infrastructure.Repositories;
using Wallet.API.Model;

namespace Wallet.API.EventHandlers
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
