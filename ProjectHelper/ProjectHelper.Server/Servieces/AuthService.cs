using ProjectHelper.Domain.Users;
using ProjectHelper.Server.Models;
using ProjectHelper.Server.Stores;

namespace ProjectHelper.Server.Servieces
{
    public class AuthService
    {
        private readonly IProductManagerRepository _productManagerRepository;
        private readonly IDeveloperRepository _developerRepository;
        private readonly TokenStore _tokenStore;
        private readonly TokenService _tokenService;


        public AuthService(IProductManagerRepository productManagerRepository, IDeveloperRepository developerRepository, TokenService tokenService, TokenStore tokenStore)
        {
            _productManagerRepository = productManagerRepository;
            _developerRepository = developerRepository;
            _tokenService = tokenService;
            _tokenStore = tokenStore;
        }

        public async Task SendConfirmationEmail(ProductManager productManager)
        {
        }

        public async Task<TokenResponse?> AuthenticateAsync(string login, string password)
        {
            IUserRepository userRepository = login.StartsWith("dev.")
            ? _developerRepository
            : _productManagerRepository;


            var userExists = await userRepository.UserIsExists(login);
            if (!userExists)
                return null;

            var isPasswordValid = await userRepository.PasswordCheck(password, login);
            if (!isPasswordValid)
                return null;

            var accessToken = _tokenService.GenerateAccessToken(login);
            var refreshToken = _tokenService.GenerateRefreshToken();

            _tokenStore.SaveRefreshToken(login, refreshToken);

            return new TokenResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
    }
}
