using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Wallet.Consumer.Models
{
    public class WalletModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public EnumTypeTransaction TypeTransaction { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }
        public EnumOrigin Origin { get; set; }
        public DateTime DateOfTransaction { get; set; }
    }

    public enum EnumOrigin
    {
        BANK,CREDIT_CARD,DEBIT_CARD,CASH
    }

    public enum EnumTypeTransaction
    {
        Debit, Credit
    }
}
