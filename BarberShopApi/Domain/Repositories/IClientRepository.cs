using BarberShopApi.Application.Requests.Barber;
using BarberShopApi.Application.Requests.Client;
using BarberShopApi.Application.Responses;
using BarberShopApi.Domain.Entities;

namespace BarberShopApi.Domain.Repositories
{
    public interface IClientRepository
    {
        Task<Response<List<BarberShop>>> GetAllBarberShops();
        Task<Response<BarberShop>> GetBarberShop(GetBarberShopRequest request);
        Task<Response<Schedule>> Schedule(ScheduleServiceRequest request);
        Task<Response<List<Schedule>>> GetUserScheduleHistory(GetUserScheduleHistory request);
    }
}
