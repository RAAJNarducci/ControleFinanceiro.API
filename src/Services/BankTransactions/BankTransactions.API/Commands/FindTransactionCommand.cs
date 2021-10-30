using BankTransactions.API.Model;
using MediatR;

namespace BankTransactions.API.Commands
{
    public class FindTransactionCommand : IRequest<Transaction>
    {
        public string Id { get; set; }
    }
}
