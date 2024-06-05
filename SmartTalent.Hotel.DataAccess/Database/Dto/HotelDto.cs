namespace SmartTalent.Hotel.DataAccess.Database.Dto
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using SmartTalent.Hotel.DataAccess.Database.Dto.Interfaces;
    using System.ComponentModel.DataAnnotations;

    public class HotelDto : IMongoEntities
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonId]
        public string Id { get; set; }


        [BsonElement("Name")]
        [Required]
        public string Name { get; set; }

        [BsonElement("Nit")]
        [Required]
        public string Nit { get; set; }

        [BsonElement("Active")]
        public bool Active { get; set; }

        //Some other properties here... 

    }
}
