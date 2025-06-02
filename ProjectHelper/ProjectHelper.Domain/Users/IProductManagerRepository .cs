using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHelper.Domain.Users
{
    public interface IProductManagerRepository: IUserRepository
    {
        Task<Company> GetCompanyByUserLogin(string login);
        Task<Company> CreateCompany(Company company, string userLogin);
    }
}
