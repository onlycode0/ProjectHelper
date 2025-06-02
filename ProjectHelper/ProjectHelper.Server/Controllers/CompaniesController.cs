using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectHelper.Domain;
using ProjectHelper.Domain.Users;
using Swashbuckle.AspNetCore.Annotations;

namespace ProjectHelper.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IProductManagerRepository _productManagerRepository;
        private readonly ILogger<CompaniesController> _logger;

        public CompaniesController(
            IProductManagerRepository productManagerRepository,
            ILogger<CompaniesController> logger)
        {
            _productManagerRepository = productManagerRepository;
            _logger = logger;
        }

        [HttpGet("current")]
        [SwaggerOperation(Summary = "Получить компанию текущего пользователя")]
        public async Task<IActionResult> GetCurrentUserCompany()
        {
            try
            {
                var username = User.Identity?.Name;
                _logger.LogInformation($"Getting company for user: {username}");

                if (string.IsNullOrEmpty(username))
                {
                    _logger.LogWarning("Username is empty");
                    return Unauthorized();
                }

                var company = await _productManagerRepository.GetCompanyByUserLogin(username);
                if (company == null)
                {
                    _logger.LogInformation($"Company not found for user: {username}");
                    return NotFound();
                }

                return Ok(company);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting company");
                throw;
            }
        }

        public class CreateCompanyRequest
        {
            public string Name { get; set; }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Создать новую компанию")]
        public async Task<IActionResult> CreateCompany([FromBody] CreateCompanyRequest request)
        {
            try
            {
                var username = User.Identity?.Name;
                _logger.LogInformation($"Creating company '{request.Name}' for user: {username}");

                if (string.IsNullOrEmpty(username))
                {
                    _logger.LogWarning("Username is empty");
                    return Unauthorized();
                }

                var company = new Company { 
                    Name = request.Name,
                    ProjectIds = new List<string>(),
                    DeveloperIds = new List<string>()
                };

                var createdCompany = await _productManagerRepository.CreateCompany(company, username);
                _logger.LogInformation($"Company created with ID: {createdCompany.Id}");
                return Ok(createdCompany);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating company");
                throw;
            }
        }
    }
} 