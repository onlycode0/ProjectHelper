using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProjectHelper.Domain.Projects
{
    public class Stage
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public int InteriorId { get; set; }

        public List<string> StepIds { get; set; } = new List<string>();
    }
}
