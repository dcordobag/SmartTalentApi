namespace SmartTalent.Hotel.DataAccessLayer.Database.Dao
{
    using SmartTalent.Hotel.DataAccess;
    using SmartTalent.Hotel.DataAccess.Database.Interfaces;
    using SmartTalent.Hotel.DataAccessLayer.Database.Dao.Interfaces;
    using SmartTalent.Hotel.DataAccessLayer.Database.Dto;

    public class ReservationDao(IDatabaseContext context) : Repository<ReservationDto>(context), IReservationDao
    {
    }
}
