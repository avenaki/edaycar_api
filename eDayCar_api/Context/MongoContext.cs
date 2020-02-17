using eDayCar_api.Entities.Base;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

public class MongoContext
    {
        private readonly IMongoDatabase _database;

        public MongoContext(string connectionString)
        {
            var mongoUrl = new MongoUrl(connectionString);
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(mongoUrl.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName) where T : Entity
        {
            return _database.GetCollection<T>(collectionName);
        }
    }


