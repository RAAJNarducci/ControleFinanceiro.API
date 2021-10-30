using System.Threading.Tasks;
using Wallet.API.Model;

namespace Wallet.API.Infrastructure.Repositories
{
    public interface IWalletRepository
    {
        Task AddAsync(WalletModel wallet);
    }
}