namespace SmartTalent.Hotel.DataAccess.Database
{
    using Microsoft.Extensions.Options;
    using MongoDB.Driver;
    using SmartTalent.Hotel.DataAccess.Database.Interfaces;


    public class DatabaseContext : IDatabaseContext
    {
        private IMongoDatabase Db { get; set; }

        private MongoClient MongoClient { get; set; }

        public DatabaseContext(IOptions<MongoDbSettings> configuration)
        {
            MongoClient = new MongoClient(configuration.Value.ConnectionString);
            Db = MongoClient.GetDatabase(configuration.Value.DatabaseName);
        }

        public IMongoCollection<TEntity> GetCollection<TEntity>(string name)
        {
            return !string.IsNullOrEmpty(name) ? Db.GetCollection<TEntity>(name) : null;
        }
    }
}
