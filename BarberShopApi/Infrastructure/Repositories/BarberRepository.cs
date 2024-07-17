using BarberShopApi.Application.Exceptions;
using BarberShopApi.Application.Requests.Barber;
using BarberShopApi.Application.Requests.Barber.AddService;
using BarberShopApi.Application.Responses;
using BarberShopApi.Domain.Entities;
using BarberShopApi.Domain.Repositories;
using BarberShopApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BarberShopApi.Infrastructure.Repositories
{
    public class BarberRepository(AppDbContext context) : IBarberRepository
    {
        private readonly AppDbContext _context = context;


        public async Task<Response<Barber>> AddBarberService(AddBarberServiceRequest request)
        {
            var barber = await _context.Barbers.FirstOrDefaultAsync(b => b.Id == request.BarberId);

            if (barber is null)
            {
                throw new NotFoundException(ResourceErrorMessages.NOT_FOUND_OBJECT);
            }

            var entityService = new Service { BarberId = request.BarberId, Name = request.Name, Price = request.Price };
            barber.Services.Add(entityService);

            _context.Services.Add(entityService);
            await _context.SaveChangesAsync();
            return new Response<Barber>(barber, 201, "Service registered with success");
        }

        public async Task<Response<Service>> DeleteBarberService(DeleteBarberServiceRequest request)
        {
            var entityService = await _context.Services.FirstOrDefaultAsync(s => s.BarberId == request.BarberId && s.Id == request.ServiceId);

            if (entityService is null)
            {
                throw new NotFoundException(ResourceErrorMessages.NOT_FOUND_OBJECT);
            }

            _context.Services.Remove(entityService);
            await _context.SaveChangesAsync();

            return new Response<Service>(entityService, 204, "service removed with success");
        }
    }
}
