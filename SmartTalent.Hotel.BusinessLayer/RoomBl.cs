namespace SmartTalent.Hotel.BusinessLayer
{
    using SmartTalent.Hotel.BusinessLayer.Interfaces;
    using SmartTalent.Hotel.DataAccessLayer.Database.Dao.Interfaces;
    using SmartTalent.Hotel.DataAccessLayer.Database.Dto;

    public class RoomBl(IRoomDao daoNegocio) : BaseBusinessRules<RoomDto, IRoomDao>(daoNegocio), IRoomBl
    {
    }
}
