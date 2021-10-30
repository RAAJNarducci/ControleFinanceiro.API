using BankTransactions.API.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankTransactions.API.Infrastructure.Repositories
{
    public interface ITransactionRepository
    {
        Task AddTransactionAsync(Transaction transaction);
        Task<IEnumerable<Transaction>> GetTransactionsAsync();
        Task<Transaction> GetTransactionAsync(string id);
    }
}
