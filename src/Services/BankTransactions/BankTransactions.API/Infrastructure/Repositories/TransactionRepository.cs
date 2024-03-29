﻿using BankTransactions.API.Model;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankTransactions.API.Infrastructure.Repositories
{
    public class TransactionRepository
        : ITransactionRepository
    {
        private readonly TransactionContext _transactionContext;

        public TransactionRepository(IOptions<TransactionSettings> settings)
        {
            _transactionContext = new TransactionContext(settings);
        }

        public async Task AddTransactionAsync(Transaction transaction)
        {
            try
            {
                Random randNum = new Random();
                var num = randNum.Next(2);
                if (num == 0)
                    await _transactionContext.Transactions.InsertOneAsync(transaction);
                else
                    throw new Exception("teste");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsAsync()
        {
            return await _transactionContext.Transactions.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<Transaction> GetTransactionAsync(string id)
        {
            var filter = Builders<Transaction>.Filter.Eq("Id", id);
            return await _transactionContext.Transactions.Find(filter).FirstOrDefaultAsync();
        }
    }
}
