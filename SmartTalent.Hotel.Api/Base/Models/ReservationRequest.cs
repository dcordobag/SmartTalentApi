namespace SmartTalent.Hotel.Api.Base.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ReservationRequest : UserDataRequest
    {
        public IList<string> RoomsId { get; set; }

        [Required]
        public string HotelId { get; set; }

        [Required]
        public DateTime CheckIn { get; set; }

        [Required]
        public DateTime CheckOut { get; set; }

        [Required]
        public string UserEmail { get; set; }

        [Required]
        public string UserPhone { get; set; }

        [Required]
        public int Adults { get; set; }

        [Required]
        public int Children { get; set; }

        [Required]
        public ReservationEmergencyContactRequest UserEmergencyContact { get; set; }

        public IList<UserDataRequest>? Guests { get; set; }
    }

    public class ReservationEmergencyContactRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Phone { get; set; }
    }

    public class UserDataRequest
    {
        [Required]
        public string UserFullName { get; set; }

        [Required]
        public string UserBirth { get; set; }

        [Required]
        public string UserGender { get; set; }

        [Required]
        public string UserDocumentType { get; set; }

        [Required]
        public string UserDocument { get; set; }
    }

    public class UpdateReservationRequest
    {
        public string UserEmail { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
    }
}
