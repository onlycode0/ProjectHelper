using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProjectHelper.Domain.Projects.Tasks
{
    public class ProjectTask
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public TaskType Type { get; set; }                  // Тип задачи ("UI", "Backend", "Testing")

        public Complexity Complexity { get; set; }              // Сложность от 1 (легко) до 5 (очень сложно), влияет на предсказание

        public List<DeveloperSkills> RequiredSkills { get; set; } = new(); // Необходимые навыки для выполнения (например, ["React", "CSS"])

        public float EstimatedDuration { get; set; }      // Изначально ожидаемое время в часах (может быть отредактировано ИИ)

        public string? AssignedEmployeeId { get; set; }   // ID сотрудника, которому назначена подзадача, или null, если не назначена

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime? StartTime { get; set; }          // Когда задача должна начаться

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime? EndTime { get; set; }
    }
}
