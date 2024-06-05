namespace SmartTalent.Hotel.DataAccess.Database
{
    using SmartTalent.Hotel.DataAccess.Database.Interfaces;

    public class MongoDbSettings : IMongoDbSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
