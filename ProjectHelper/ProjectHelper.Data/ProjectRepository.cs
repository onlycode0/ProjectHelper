using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ProjectHelper.Domain.Projects;

namespace ProjectHelper.Data
{
    public class ProjectRepository
    {
        private readonly IMongoCollection<Project> _projectRepository;

        public ProjectRepository(IOptions<MongoDBSettingsModel> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DataBaseName);
            _projectRepository = database.GetCollection<Project>(mongoDBSettings.Value.ProjectsCollection);
        }

        public async Task CreatAsync(Project project)
        {
            await _projectRepository.InsertOneAsync(new Project
            {
                Name = project.Name,
                Description = project.Description,
                CreateDate = project.CreateDate,
                DeveloperIds = project.DeveloperIds,
                Priority = project.Priority,
                Status = project.Status
            });
            return;
        }
    }
}
