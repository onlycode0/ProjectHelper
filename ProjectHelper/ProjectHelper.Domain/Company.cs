using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProjectHelper.Domain
{
    public class Company
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }

        public List<string> ProjectIds { get; set; }

        public List<string> DeveloperIds { get; set; }

    }
}
