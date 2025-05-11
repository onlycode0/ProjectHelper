using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ProjectHelper.Domain.Projects;

namespace ProjectHelper.Data
{
    public class StageRepository
    {
        private readonly IMongoCollection<Stage> _stageRepository;

        public StageRepository(IOptions<MongoDBSettingsModel> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DataBaseName);
            _stageRepository = database.GetCollection<Stage>(mongoDBSettings.Value.StagesCollection);
        }

        public async Task CreatAsync(Stage stage)
        {
            await _stageRepository.InsertOneAsync(new Stage
            {
                InteriorId = stage.InteriorId,
                StepIds = stage.StepIds,
                Name = stage.Name,
                Description = stage.Description,
            });
            return;
        }
    }
}
