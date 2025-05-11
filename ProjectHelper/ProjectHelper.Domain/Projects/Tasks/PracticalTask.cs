using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHelper.Domain.Projects.Tasks
{
    public class PracticalTask: Task
    {
        public string Type { get; set; }                  // Тип задачи ("UI", "Backend", "Testing")

        public int Complexity { get; set; }              // Сложность от 1 (легко) до 5 (очень сложно), влияет на предсказание

        public List<DeveloperSkills> RequiredSkills { get; set; } = new(); // Необходимые навыки для выполнения (например, ["React", "CSS"])

        public float EstimatedDuration { get; set; }      // Изначально ожидаемое время в часах (может быть отредактировано ИИ)

        public string? AssignedEmployeeId { get; set; }   // ID сотрудника, которому назначена подзадача, или null, если не назначена

        public DateTime? StartTime { get; set; }          // Когда задача должна начаться

        public DateTime? EndTime { get; set; }
    }
}
