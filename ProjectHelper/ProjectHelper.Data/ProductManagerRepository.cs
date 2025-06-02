using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using ProjectHelper.Domain;
using ProjectHelper.Domain.Users;
using Microsoft.Extensions.Logging;

namespace ProjectHelper.Data
{
    public class ProductManagerRepository: IProductManagerRepository
    {
        private readonly IMongoCollection<ProductManager> _productManagersRepository;
        private readonly IMongoCollection<Company> _companiesRepository;
        private readonly ILogger<ProductManagerRepository> _logger;

        public ProductManagerRepository(
            IOptions<MongoDBSettingsModel> mongoDBSettings,
            ILogger<ProductManagerRepository> logger)
        {
            _logger = logger;
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DataBaseName);
            _productManagersRepository = database.GetCollection<ProductManager>(mongoDBSettings.Value.ProductManagersCollection);
            _companiesRepository = database.GetCollection<Company>(mongoDBSettings.Value.CompaniesCollection);
        }

        public async Task CreatAsync(ProductManager productManager)
        {
            await _productManagersRepository.InsertOneAsync(new ProductManager
            {
                Name = productManager.Name,
                Login = productManager.Login,
                Password = productManager.Password,
                RegistrationDate = productManager.RegistrationDate,
            });
        }

        public async Task<bool>UserIsExists(string login)
        {
            var filter = Builders<ProductManager>.Filter.Eq(u => u.Login, login);
            var productManager = await _productManagersRepository.Find(filter).FirstOrDefaultAsync();
            return productManager != null;
        }

        public async Task<ProductManager> GetProductManagerByLogin(string login)
        {
            FilterDefinition<ProductManager> filter = Builders<ProductManager>.Filter.Eq(u => u.Login, login);

            return await _productManagersRepository.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<bool> PasswordCheck(string password, string login)
        {
            var filter = Builders<ProductManager>.Filter.And(
                Builders<ProductManager>.Filter.Eq(u => u.Login, login),
                Builders<ProductManager>.Filter.Eq(u => u.Password, password)
            );

            var productManager = await _productManagersRepository.Find(filter).FirstOrDefaultAsync();

            if (productManager != null)
            {
                return true;
            }

            return false;
        }

        public async Task<Company> GetCompanyByUserLogin(string login)
        {
            _logger.LogInformation($"Getting company for user: {login}");
            
            var manager = await GetProductManagerByLogin(login);
            _logger.LogInformation($"Found manager: {manager?.Id}, CompanyId: {manager?.CompanyId}");
            
            if (manager == null || string.IsNullOrEmpty(manager.CompanyId))
            {
                _logger.LogWarning($"Manager not found or has no company ID");
                return null;
            }

            var filter = Builders<Company>.Filter.Eq("_id", ObjectId.Parse(manager.CompanyId));
            var company = await _companiesRepository.Find(filter).FirstOrDefaultAsync();
            
            _logger.LogInformation($"Found company: {company?.Id}, Name: {company?.Name}");
            return company;
        }

        public async Task<Company> CreateCompany(Company company, string userLogin)
        {
            company.Id = ObjectId.GenerateNewId().ToString();
            _logger.LogInformation($"Creating company with ID: {company.Id}");
            
            await _companiesRepository.InsertOneAsync(company);
            
            var manager = await GetProductManagerByLogin(userLogin);
            if (manager != null)
            {
                var filter = Builders<ProductManager>.Filter.Eq(pm => pm.Id, manager.Id);
                var update = Builders<ProductManager>.Update.Set(pm => pm.CompanyId, company.Id);
                await _productManagersRepository.UpdateOneAsync(filter, update);
                _logger.LogInformation($"Updated manager {manager.Id} with company ID {company.Id}");
            }

            return company;
        }
    }
}
