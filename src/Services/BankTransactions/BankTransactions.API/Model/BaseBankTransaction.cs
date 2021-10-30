using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankTransactions.API.Model
{
    public abstract class BaseBankTransaction
    {
        public string IdTransaction { get; set; }
        public DateTime? DateTransaction { get; set; }
    }
}
