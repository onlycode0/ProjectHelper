using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ProjectHelper.Domain.Projects;

namespace ProjectHelper.Data
{
    public class StepRepository
    {
        private readonly IMongoCollection<Step> _stepRepository;

        public StepRepository(IOptions<MongoDBSettingsModel> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DataBaseName);
            _stepRepository = database.GetCollection<Step>(mongoDBSettings.Value.StepsCollection);
        }

        public async Task CreatAsync(Step step)
        {
            await _stepRepository.InsertOneAsync(new Step
            {
                TaskIds = step.TaskIds,
            });
            return;
        }
    }
}
