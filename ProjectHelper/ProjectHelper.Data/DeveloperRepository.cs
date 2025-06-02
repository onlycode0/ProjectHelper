using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ProjectHelper.Domain.Users;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;

namespace ProjectHelper.Data
{
    public class DeveloperRepository: IDeveloperRepository
    {
        private readonly IMongoCollection<Developer> _developerRepository;
        private readonly ILogger<DeveloperRepository> _logger;

        public DeveloperRepository(
            IOptions<MongoDBSettingsModel> mongoDBSettings,
            ILogger<DeveloperRepository> logger)
        {
            _logger = logger;
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DataBaseName);
            _developerRepository = database.GetCollection<Developer>(mongoDBSettings.Value.DevelopersCollection);
        }

        public async Task<IEnumerable<Developer>> GetAllAsync()
        {
            _logger.LogInformation("Getting all developers");
            var developers = await _developerRepository.Find(_ => true).ToListAsync();
            _logger.LogInformation($"Found {developers.Count} developers in total");
            return developers;
        }

        public async Task<IEnumerable<Developer>> GetByCompanyIdAsync(string companyId)
        {
            _logger.LogInformation($"Getting developers for company: {companyId}");
            
            if (string.IsNullOrEmpty(companyId))
            {
                _logger.LogWarning("CompanyId is null or empty");
                return new List<Developer>();
            }

            var filter = Builders<Developer>.Filter.Eq(d => d.CompanyId, companyId);
            var developers = await _developerRepository.Find(filter).ToListAsync();
            
            _logger.LogInformation($"Found {developers.Count} developers for company {companyId}");
            foreach (var dev in developers)
            {
                _logger.LogInformation($"Developer: {dev.Name}, CompanyId: {dev.CompanyId}");
            }
            return developers;
        }

        public async Task CreateAsync(Developer developer)
        {
            _logger.LogInformation($"Creating developer: {developer.Name}, CompanyId: {developer.CompanyId}");
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
                ProjectIds = new List<string>(),
                Schedule = new Dictionary<DateTime, float>()
            });
            _logger.LogInformation("Developer created successfully");
        }

        public async Task<bool> UserIsExists(string login)
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
            return Developer != null;
        }
    }
}
