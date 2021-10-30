using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace BankTransactions.API.Model
{
    public class Transaction
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public enumTypeTransaction TypeTransaction { get; set; }
        
        [Required(ErrorMessage ="Description is required")]
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
