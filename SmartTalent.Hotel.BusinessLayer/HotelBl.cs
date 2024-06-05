namespace SmartTalent.Hotel.BusinessLayer
{
    using SmartTalent.Hotel.BusinessLayer.Interfaces;
    using SmartTalent.Hotel.DataAccess.Database.Dao.Interfaces;
    using SmartTalent.Hotel.DataAccess.Database.Dto;

    public class HotelBl(IHotelDao daoNegocio) : BaseBusinessRules<HotelDto, IHotelDao>(daoNegocio), IHotelBl
    {

    }
}
