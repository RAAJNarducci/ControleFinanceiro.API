using System;

namespace Wallet.Consumer.Models
{
    public class Transaction
    {
        public string Id { get; set; }
        public EnumTypeTransaction TypeTransaction { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }
        public Bank Bank { get; set; }
        public DateTime DateOfTransaction { get; set; }
    }
}
