namespace SmartTalent.Hotel.DataAccessLayer.Database.Dto
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using SmartTalent.Hotel.DataAccess.Database.Dto.Interfaces;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ReservationDto : UserDataDto, IMongoEntities
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonId]
        public string Id { get; set; }

        [BsonElement("RoomsId")]
        [Required]
        public IList<string> RoomsId { get; set; }

        [BsonElement("HotelId")]
        [Required]
        public string HotelId { get; set; }

        [BsonElement("CheckIn")]
        [Required]
        public DateTime CheckIn { get; set; }

        [BsonElement("CheckOut")]
        [Required]
        public DateTime CheckOut { get; set; }

        [BsonElement("UserEmail")]
        [Required]
        public string UserEmail { get; set; }

        [BsonElement("UserPhone")]
        [Required]
        public string UserPhone { get; set; }


        [BsonElement("Adults")]
        [Required]
        public int Adults { get; set; }

        [BsonElement("Children")]
        [Required]
        public int Children { get; set; }

        [BsonElement("UserEmergencyContact")]
        [Required]
        public ReservationEmergencyContactDto UserEmergencyContact { get; set; }

        [BsonElement("Guests")]
        public IList<UserDataDto> Guests { get; set; }
    }

    public class ReservationEmergencyContactDto
    {
        [BsonElement("Name")]
        [Required]
        public string Name { get; set; }

        [BsonElement("Phone")]
        [Required]
        public string Phone { get; set; }
    }

    public class UserDataDto
    {
        [BsonElement("UserFullName")]
        [Required]
        public string UserFullName { get; set; }

        [BsonElement("UserBirth")]
        [Required]
        public string UserBirth { get; set; }

        [BsonElement("UserGender")]
        [Required]
        public string UserGender { get; set; }

        [BsonElement("UserDocumentType")]
        [Required]
        public string UserDocumentType { get; set; }

        [BsonElement("UserDocument")]
        [Required]
        public string UserDocument { get; set; }
    }
}
