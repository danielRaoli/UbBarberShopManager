using BarberShopApi.Application.Requests.Auth;
using BarberShopApi.Application.Responses;

namespace BarberShopApi.Domain.Repositories
{
    public interface IAuthRepository
    {
        Task<Response<string>> Login(LoginRequest request);
        Task<Response<string>> Register(RegisterAccountRequest request);
    }
}
