using BankTransactions.API.Model;
using MediatR;

namespace BankTransactions.API.Commands
{
    public class NewTransactionCommand : IRequest<bool>
    {
        public Transaction Transaction { get; private set; }

        public NewTransactionCommand(Transaction transaction)
        {
            Transaction = transaction;
        }
    }
}
