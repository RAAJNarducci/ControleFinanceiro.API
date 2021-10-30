using BankTransactions.API.Model;
using System.Threading.Tasks;

namespace BankTransactions.API.Services
{
    public interface IBankTransactionService
    {
        Task SyncTransaction(Transaction transaction);
    }
}