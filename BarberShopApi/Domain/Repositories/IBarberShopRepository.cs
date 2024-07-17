using BarberShopApi.Application.Requests.Barber;
using BarberShopApi.Application.Requests.Barber.CreatedBarber;
using BarberShopApi.Application.Requests.Barber.EditBarber;
using BarberShopApi.Application.Responses;
using BarberShopApi.Domain.Entities;

namespace BarberShopApi.Domain.Repositories
{
    public interface IBarberShopRepository
    {
        Task<Response<BarberShop>> CreateBarberShop(CreateBarberShopRequest request);
        Task<Response<BarberShop>> GetBarberShop(GetBarberShopRequest request);
        Task<Response<List<BarberShop>>> GetAllBarberShops();
        Task<Response<BarberShop>> UpdateBarberShop(UpdateBarberShopRequest request);
        Task<Response<BarberShop>> DeleteBarberShop(DeleteBarberShopRequest request);
        Task<Response<Barber>> CreateBarber(CreateBarberRequest request);
        Task<Response<Barber>> EditBarber(EditBarberRequest request);
        Task<Response<Barber>> DeleteBarber(DeleteBarberRequest request);

    }
}
