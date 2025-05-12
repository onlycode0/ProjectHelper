namespace ProjectHelper.Domain.Users
{
    public interface IUserRepository
    {
        Task<bool> UserIsExists(string login);
        Task<bool> PasswordCheck(string password, string login);
    }
}
