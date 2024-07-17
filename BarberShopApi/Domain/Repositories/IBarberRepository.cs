using BarberShopApi.Application.Requests.Barber;
using BarberShopApi.Application.Requests.Barber.AddService;
using BarberShopApi.Application.Responses;
using BarberShopApi.Domain.Entities;

namespace BarberShopApi.Domain.Repositories
{
    public interface IBarberRepository
    {
        Task<Response<Barber>> AddBarberService(AddBarberServiceRequest request);
        Task<Response<Service>> DeleteBarberService(DeleteBarberServiceRequest request);
    }
}
