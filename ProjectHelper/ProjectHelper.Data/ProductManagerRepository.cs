using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ProjectHelper.Domain.Users;

namespace ProjectHelper.Data
{
    public class ProductManagerRepository
    {
        private readonly IMongoCollection<ProductManager> _productManagersRepository;

        public ProductManagerRepository(IOptions<MongoDBSettingsModel> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DataBaseName);
            _productManagersRepository = database.GetCollection<ProductManager>(mongoDBSettings.Value.ProductManagersCollection);
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
            return;
        }

        public async Task<bool> ProductManagerIsExists(string login)
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
    }
}
