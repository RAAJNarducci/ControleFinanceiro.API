using BankTransactions.API.Model;
using MediatR;
using System.Collections.Generic;

namespace BankTransactions.API.Commands
{
    public class FindAllTransactionCommand : IRequest<IEnumerable<Transaction>>
    { }
}
