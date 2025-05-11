using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ProjectHelper.Domain.Users;

namespace ProjectHelper.Data
{
    public class DeveloperRepository
    {
        private readonly IMongoCollection<Developer> _developerRepository;

        public DeveloperRepository(IOptions<MongoDBSettingsModel> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DataBaseName);
            _developerRepository = database.GetCollection<Developer>(mongoDBSettings.Value.DevelopersCollection);
        }

        public async Task CreatAsync(Developer developer)
        {
            await _developerRepository.InsertOneAsync(new Developer
            {
                Name = developer.Name,
                Login = developer.Login,
                Password = developer.Password,
                RegistrationDate = developer.RegistrationDate,
                CompanyId = developer.CompanyId,    
                Skills = developer.Skills,
                Experience = developer.Experience,
                DailyCapacity = developer.DailyCapacity,
            });
            return;
        }

        public async Task<bool> DeveloperIsExists(string login)
        {
            var filter = Builders<Developer>.Filter.Eq(u => u.Login, login);
            var Developer = await _developerRepository.Find(filter).FirstOrDefaultAsync();
            return Developer != null;
        }

        public async Task<Developer> GetDeveloperByLogin(string login)
        {
            FilterDefinition<Developer> filter = Builders<Developer>.Filter.Eq(u => u.Login, login);

            return await _developerRepository.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<bool> PasswordCheck(string password, string login)
        {
            var filter = Builders<Developer>.Filter.And(
                Builders<Developer>.Filter.Eq(u => u.Login, login),
                Builders<Developer>.Filter.Eq(u => u.Password, password)
            );

            var Developer = await _developerRepository.Find(filter).FirstOrDefaultAsync();

            if (Developer != null)
            {
                return true;
            }

            return false;
        }
    }
}
