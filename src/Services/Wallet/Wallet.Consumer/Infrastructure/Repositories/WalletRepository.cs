using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Wallet.Consumer.Models;

namespace Wallet.Consumer.Infrastructure.Repositories
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
