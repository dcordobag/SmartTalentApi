
namespace SmartTalent.Hotel.DataAccessLayer.Database.Dao
{
    using SmartTalent.Hotel.DataAccess;
    using SmartTalent.Hotel.DataAccess.Database.Interfaces;
    using SmartTalent.Hotel.DataAccessLayer.Database.Dao.Interfaces;
    using SmartTalent.Hotel.DataAccessLayer.Database.Dto;

    public class RoomDao(IDatabaseContext context) : Repository<RoomDto>(context), IRoomDao
    {
    }
}
