using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace ProjectHelper.Domain.Users
{
    public class Developer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string CompanyId { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime RegistrationDate { get; set; }

        [JsonConverter(typeof(SkillsDictionaryJsonConverter))]
        public Dictionary<string, string> Skills { get; set; } = new();

        public int Experience { get; set; }

        public int DailyCapacity { get; set; }  //занятость

        public Dictionary<DateTime, float> Schedule { get; set; } = new();         //расписание дата-колво часов

        public List<string> ProjectIds { get; set; } = new();
    }
}
