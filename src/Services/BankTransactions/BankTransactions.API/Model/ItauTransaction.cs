using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankTransactions.API.Model
{
    public class ItauTransaction : BaseBankTransaction
    {
        public StatusTransaction StatusTransaction { get; set; }
    }

    public enum StatusTransaction
    {
        CONFIRMED, WAITING, NOT_FOUND
    }
}
