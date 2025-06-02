namespace ProjectHelper.Domain.Users
{
    public interface IDeveloperRepository: IUserRepository
    {
        Task<IEnumerable<Developer>> GetAllAsync();
        Task<IEnumerable<Developer>> GetByCompanyIdAsync(string companyId);
        Task CreateAsync(Developer developer);
    }
}
