using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectHelper.Domain.Users;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json;
using ProjectHelper.Domain;

namespace ProjectHelper.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DevelopersController : ControllerBase
    {
        private readonly IDeveloperRepository _developerRepository;
        private readonly IProductManagerRepository _productManagerRepository;
        private readonly ILogger<DevelopersController> _logger;

        public DevelopersController(
            IDeveloperRepository developerRepository, 
            IProductManagerRepository productManagerRepository,
            ILogger<DevelopersController> logger)
        {
            _developerRepository = developerRepository;
            _productManagerRepository = productManagerRepository;
            _logger = logger;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Получить список разработчиков текущей компании")]
        public async Task<IActionResult> GetDevelopers()
        {
            try
            {
                var username = User.Identity?.Name;
                _logger.LogInformation($"Current user: {username}");

                if (string.IsNullOrEmpty(username))
                {
                    _logger.LogWarning("User is not authorized");
                    return Unauthorized("Пользователь не авторизован");
                }

                var company = await _productManagerRepository.GetCompanyByUserLogin(username);
                _logger.LogInformation($"Found company: {company?.Id ?? "null"}");

                if (company == null)
                {
                    _logger.LogWarning($"Company not found for user {username}");
                    return NotFound("Компания не найдена");
                }

                var developers = await _developerRepository.GetByCompanyIdAsync(company.Id);
                var developersList = developers.ToList();
                _logger.LogInformation($"Found {developersList.Count} developers for company {company.Id}");
                foreach (var dev in developersList)
                {
                    _logger.LogInformation($"Developer: {dev.Name}, CompanyId: {dev.CompanyId}");
                }

                return Ok(developers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting developers");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Создать нового разработчика")]
        public async Task<IActionResult> CreateDeveloper([FromBody] Developer developer)
        {
            try
            {
                _logger.LogInformation($"Received developer data: {JsonSerializer.Serialize(developer)}");

                if (developer.Skills == null)
                {
                    _logger.LogWarning("Skills dictionary is null");
                    return BadRequest("Skills dictionary cannot be null");
                }

                var userExists = await _developerRepository.UserIsExists(developer.Login);
                if (userExists)
                {
                    return BadRequest("Разработчик с таким логином уже существует");
                }

                var username = User.Identity?.Name;
                _logger.LogInformation($"Creating developer for user: {username}");

                if (string.IsNullOrEmpty(username))
                {
                    _logger.LogWarning("User is not authorized");
                    return Unauthorized("Пользователь не авторизован");
                }

                var company = await _productManagerRepository.GetCompanyByUserLogin(username);
                _logger.LogInformation($"Found company: {company?.Id ?? "null"}");

                if (company == null)
                {
                    _logger.LogWarning($"Company not found for user {username}");
                    return NotFound("Компания не найдена");
                }

                developer.CompanyId = company.Id;
                _logger.LogInformation($"Setting CompanyId: {company.Id} for new developer");

                // Преобразуем строковые значения в enum и создаем новый словарь
                var convertedSkills = new Dictionary<string, string>();
                foreach (var skill in developer.Skills)
                {
                    if (Enum.TryParse<DeveloperSkills>(skill.Key, out var skillEnum) && 
                        Enum.TryParse<SkillsLevel>(skill.Value, out var levelEnum))
                    {
                        convertedSkills[skillEnum.ToString()] = levelEnum.ToString();
                    }
                    else
                    {
                        _logger.LogWarning($"Invalid skill or level: Key={skill.Key}, Value={skill.Value}");
                        return BadRequest($"Invalid skill or level values: {skill.Key} - {skill.Value}");
                    }
                }
                developer.Skills = convertedSkills;

                await _developerRepository.CreateAsync(developer);
                return Ok(developer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating developer");
                return StatusCode(500, "Internal server error");
            }
        }
    }
} 