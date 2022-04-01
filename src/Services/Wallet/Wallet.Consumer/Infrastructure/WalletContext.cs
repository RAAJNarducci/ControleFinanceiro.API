using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Wallet.Consumer.Models;

namespace Wallet.Consumer.Infrastructure
{
    public class WalletContext
    {
        private readonly IMongoDatabase _database = null;

        public WalletContext(IOptions<WalletSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<WalletModel> Wallets
        {
            get
            {
                return _database.GetCollection<WalletModel>("Wallets");
            }
        }
    }
}
