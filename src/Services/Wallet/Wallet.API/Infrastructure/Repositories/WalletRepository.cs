using BankTransactions.API;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Wallet.API.Model;

namespace Wallet.API.Infrastructure.Repositories
{
    public class WalletRepository
        : IWalletRepository
    {
        private readonly WalletContext _walletContext;

        public WalletRepository(IOptions<WalletSettings> settings)
        {
            _walletContext = new WalletContext(settings);
        }

        public async Task AddAsync(WalletModel wallet)
        {
            await _walletContext.Wallets.InsertOneAsync(wallet);
        }
    }
}
