using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ProjectHelper.Domain.Projects;
using ProjectHelper.Domain.Projects.Tasks;

namespace ProjectHelper.Data
{
    public class ProjectTaskRepository
    {
        private readonly IMongoCollection<ProjectTask> _projectTaskRepository;

        public ProjectTaskRepository(IOptions<MongoDBSettingsModel> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DataBaseName);
            _projectTaskRepository = database.GetCollection<ProjectTask>(mongoDBSettings.Value.ProjectTasksCollection);
        }

        public async Task CreatAsync(ProjectTask projectTask)
        {
            await _projectTaskRepository.InsertOneAsync(new ProjectTask
            {
                Name = projectTask.Name,
                Description = projectTask.Description,
                Type = projectTask.Type,
                Complexity = projectTask.Complexity,
                RequiredSkills = projectTask.RequiredSkills,
                AssignedEmployeeId = projectTask.AssignedEmployeeId,
                StartTime = projectTask.StartTime,
            });
            return;
        }
    }
}
