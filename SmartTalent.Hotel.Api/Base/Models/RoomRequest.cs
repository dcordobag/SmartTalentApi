namespace SmartTalent.Hotel.Api.Base.Models
{
    using System.ComponentModel.DataAnnotations;


    public class RoomRequest
    {
        public string? RoomName { get; set; }
        public string? RoomType { get; set; }
        public string? RoomDescription { get; set; }
        public string? RoomLocation { get; set; }
        public decimal RoomPrice { get; set; }
        public decimal RoomPriceTax { get; set; }
        public int RoomCapacity { get; set; }
        [Required]
        public bool Active { get; set; }
        public string? HotelId { get; set; }
        public string? HotelName { get; set; }
    }

    public class AssingRoomRequest
    {
        [Required]
        public string RoomId { get; set; }

        [Required]
        public string HotelId { get; set; }
        [Required]
        public string HotelName { get; set; }
    }

    public class FilterRoomModel
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOut { get; set; }
        public int QuantityPeople { get; set; }
        public string DestinationCity { get; set; }
    }

    public class BookRoomRequest
    {
        public string RoomId { get; set; }

        public string ReservationId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
    }
}
