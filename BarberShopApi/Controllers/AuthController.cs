using BarberShopApi.Application.Requests.Auth;
using BarberShopApi.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BarberShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthRepository repository) : ControllerBase
    {
        private readonly IAuthRepository _repository = repository;
        
        [HttpPost("/register")]
        public async Task<IActionResult> Register([FromBody] RegisterAccountRequest request)
        {
            var response = await _repository.Register(request);
            return Ok(response);    
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var response =await _repository.Login(request);
            return Ok(response);
        }
    }
}
         