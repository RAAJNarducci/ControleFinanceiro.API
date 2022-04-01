using System.Threading.Tasks;
using Wallet.Consumer.Models;

namespace Wallet.Consumer.Infrastructure.Repositories
{
    public interface IWalletRepository
    {
        Task AddAsync(WalletModel wallet);
    }
}