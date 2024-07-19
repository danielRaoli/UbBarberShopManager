using BarberShopApi.Application.Exceptions;
using BarberShopApi.Application.Requests.Auth;
using BarberShopApi.Application.Responses;
using BarberShopApi.Domain.Entities;
using BarberShopApi.Domain.Repositories;
using BarberShopApi.Domain.Services;
using BarberShopApi.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;

namespace BarberShopApi.Infrastructure.Repositories
{
    public class AuthRepository(AppDbContext context, UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService) : IAuthRepository
    {

        private readonly AppDbContext _context = context;
        private readonly UserManager<User> _userManager = userManager;
        private readonly SignInManager<User> _signManager = signInManager;
        private readonly ITokenService _tokenService = tokenService;


        public async Task<Response<string>> Login(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                throw new NotFoundException(ResourceErrorMessages.NOT_FOUND_OBJECT);
            }

            var result = await _signManager.PasswordSignInAsync(user, request.Password, false, false);

            if (result.Succeeded is false)
            {
                throw new Exception("Unauthorize");
            }

            var role = await _userManager.GetRolesAsync(user);

            var token = _tokenService.GenerateToken(user, role[0]);

            return new Response<string>(token, 200);

        }

        public async Task<Response<string>> Register(RegisterAccountRequest request)
        {
            var user = new User { Email = request.Email, UserName = request.Email };
            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded is false)
            {
                throw new Exception("internal error, try again latter");
            }

            await _userManager.AddToRoleAsync(user,request.Role.ToString());


            return new Response<string>("register with success", 201);
        }
    }
}
