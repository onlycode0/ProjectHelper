using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProjectHelper.Domain.Projects
{
    public class Project
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreateDate { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime DeadLine { get; set; }

        public Priority Priority { get; set; }

        public Status Status { get; set; }

        public List<string> DeveloperIds { get; set; } =  new List<string>();

        public List<string> StageIds { get; set; } = new List<string>();
    }
}
