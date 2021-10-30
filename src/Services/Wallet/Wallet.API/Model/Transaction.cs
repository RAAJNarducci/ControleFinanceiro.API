using System;

namespace Wallet.API.Model
{
    public class Transaction
    {
        public string Id { get; set; }
        public enumTypeTransaction TypeTransaction { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }
        public Bank Bank { get; set; }
        public DateTime DateOfTransaction { get; set; }
    }

    public enum enumTypeTransaction
    {
        Debit,Credit
    }
}
