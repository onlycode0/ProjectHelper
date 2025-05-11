using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProjectHelper.Domain.Users
{
    public class ProductManager
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string CompanyId { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime RegistrationDate { get; set; }
    }
}
