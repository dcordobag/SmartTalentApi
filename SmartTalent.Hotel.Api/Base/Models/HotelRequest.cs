namespace SmartTalent.Hotel.Api.Base.Models
{
    using System.ComponentModel.DataAnnotations;

    public class HotelRequest
    {
        public string? HotelName { get; set; }
        public string? HotelNit { get; set; }
    }

    public class StatusRequest
    {
        [Required]
        public bool Active { get; set; }
    }
}
