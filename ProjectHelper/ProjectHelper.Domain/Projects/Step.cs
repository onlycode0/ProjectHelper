using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProjectHelper.Domain.Projects
{
    public class Step
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public List<string> TaskIds { get; set; } = new List<string>();
    }
}
