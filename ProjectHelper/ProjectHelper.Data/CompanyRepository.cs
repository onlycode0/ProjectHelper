using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ProjectHelper.Domain;

namespace ProjectHelper.Data
{
    public class CompanyRepository
    {
        private readonly IMongoCollection<Company> _сompanyRepository;

        public CompanyRepository(IOptions<MongoDBSettingsModel> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DataBaseName);
            _сompanyRepository = database.GetCollection<Company>(mongoDBSettings.Value.CompaniesCollection);
        }

        public async Task CreatAsync(Company company)
        {
            await _сompanyRepository.InsertOneAsync(new Company
            {
                Name = company.Name,
                DeveloperIds = company.DeveloperIds,
            });
            return;
        }


    }
}
