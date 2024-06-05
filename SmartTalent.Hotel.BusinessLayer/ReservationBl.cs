namespace SmartTalent.Hotel.BusinessLayer
{
    using SmartTalent.Hotel.BusinessLayer.Interfaces;
    using SmartTalent.Hotel.DataAccessLayer.Database.Dao.Interfaces;
    using SmartTalent.Hotel.DataAccessLayer.Database.Dto;

    public class ReservationBl(IReservationDao daoNegocio) : BaseBusinessRules<ReservationDto, IReservationDao>(daoNegocio), IReservationBl
    {
    }
}
