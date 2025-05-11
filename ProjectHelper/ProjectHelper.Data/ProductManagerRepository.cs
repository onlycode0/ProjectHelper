using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using ProjectHelper.Domain.Users;

namespace ProjectHelper.Data
{
    public class ProductManagerRepository
    {
        private readonly IMongoCollection<ProductManager> _productManagersCollection;

        public ProductManagerRepository(IOptions<MongoDBSettingsModel> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DataBaseName);
            _productManagersCollection = database.GetCollection<ProductManager>(mongoDBSettings.Value.ProductManagersCollection);
        }


        public async Task CreatAsync(ProductManager productManager)
        {
            await _productManagersCollection.InsertOneAsync(new ProductManager
            {
                Name = productManager.Name,
                Login = productManager.Login,
                Password = productManager.Password,
                RegistrationDate = productManager.RegistrationDate,
            });
            return;
        }

    }
}
