using BarberShopApi.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace BarberShopApi.Domain.Services
{
    public interface ITokenService
    {
        public string GenerateToken(User user, string role);
    }
}
