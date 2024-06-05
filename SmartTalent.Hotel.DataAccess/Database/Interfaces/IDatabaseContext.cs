
namespace SmartTalent.Hotel.DataAccess.Database.Interfaces
{
    using MongoDB.Driver;

    public interface IDatabaseContext
    {
        IMongoCollection<TEntity> GetCollection<TEntity>(string name);
    }
}
