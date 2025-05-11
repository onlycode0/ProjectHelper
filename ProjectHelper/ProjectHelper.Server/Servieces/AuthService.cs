using ProjectHelper.Data;
using ProjectHelper.Domain.Users;

namespace ProjectHelper.Server.Servieces
{
    public class AuthService
    {
        private readonly ProductManagerRepository _productManagerRepository;
        private readonly DeveloperRepository _developerRepository;

        public AuthService(ProductManagerRepository productManagerRepository, DeveloperRepository developerRepository)
        {
            _productManagerRepository = productManagerRepository;
            _developerRepository = developerRepository;
        }

        public async Task SendConfirmationEmail(ProductManager productManager)
        {
        }
    }
}
