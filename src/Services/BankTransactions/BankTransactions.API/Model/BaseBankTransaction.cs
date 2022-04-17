using System;

namespace BankTransactions.API.Model
{
    public abstract class BaseBankTransaction
    {
        public string IdTransaction { get; set; }
        public DateTime? DateTransaction { get; set; }
    }
}
