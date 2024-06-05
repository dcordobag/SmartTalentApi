namespace SmartTalent.Hotel.DataAccess.Database.Dao
{
    using SmartTalent.Hotel.DataAccess.Database.Dao.Interfaces;
    using SmartTalent.Hotel.DataAccess.Database.Dto;
    using SmartTalent.Hotel.DataAccess.Database.Interfaces;

    public class HotelDao(IDatabaseContext context) : Repository<HotelDto>(context), IHotelDao
    {
    }
}
