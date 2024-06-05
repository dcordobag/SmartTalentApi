namespace SmartTalent.Hotel.DataAccessLayer.Database.Dto
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using SmartTalent.Hotel.DataAccess.Database.Dto.Interfaces;
    using System.ComponentModel.DataAnnotations;

    public class RoomDto : IMongoEntities
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonId]
        public string Id { get; set; }


        [BsonElement("Name")]
        [Required]
        public string Name { get; set; }

        [BsonElement("Type")]
        [Required]
        public string Type { get; set; }

        [BsonElement("Description")]
        [Required]
        public string Description { get; set; }

        [BsonElement("Location")]
        [Required]
        public string Location { get; set; }

        [BsonElement("MaxCapacity")]
        [Required]
        public int MaxCapacity { get; set; }

        [BsonElement("Active")]
        [Required]
        public bool Active { get; set; }

        [BsonElement("BasePrice")]
        [Required]
        public decimal BasePrice { get; set; }

        [BsonElement("Tax")]
        [Required]
        public decimal Tax { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("HotelId")]
        public string HotelId { get; set; }

        [BsonElement("HotelName")]
        public string HotelName { get; set; }

        [BsonElement("ReservatedDates")]
        public IList<ReservatedDates> ReservatedDates { get; set; }

        //Some other properties here...

    }

    public class ReservatedDates
    {
        [BsonElement("ReservationId")]
        public string ReservationId { get; set; }

        [BsonElement("CheckInDate")]
        public DateTime CheckInDate { get; set; }

        [BsonElement("CheckOut")]
        public DateTime CheckOut { get; set; }
    }
}
