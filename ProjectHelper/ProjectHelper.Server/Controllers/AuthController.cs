using DnsClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectHelper.Domain.Users;
using ProjectHelper.Server.Models;
using ProjectHelper.Server.Servieces;
using Swashbuckle.AspNetCore.Annotations;
using ProjectHelper.Server.Stores;

namespace ProjectHelper.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("send-confirm-email")]
        [SwaggerOperation(Summary = "Отправка на почту ссылки для подтверждения")]
        [SwaggerResponse(200, "Success.")]
        [SwaggerResponse(409, "Conflict")]
        public async Task<IActionResult> SendConfirmationEmailAsync([FromBody] RegModel model)
        {
            ProductManager productManager = new ProductManager
            {
                Name = model.Name,
                Login = model.Login,
                Password = model.Password,
                RegistrationDate = model.RegistrationDate,
            };

            await _authService.SendConfirmationEmail(productManager);

            return Ok();

        }

        [HttpPost("login")]
        [SwaggerOperation(Summary = "Аутентификация пользователя/админа")]
        [SwaggerResponse(401, "Invalid credentials")]
        public async Task<IActionResult> LoginAsync([FromBody] LogModel model)
        {
            var token = await _authService.AuthenticateAsync(model.Login, model.Password);

            if (token != null)
                return Ok(token);

            return Unauthorized();
        }

    }

}
