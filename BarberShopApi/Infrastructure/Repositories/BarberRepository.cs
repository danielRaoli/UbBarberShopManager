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
            var barberShop = await _context.BarberShops.Include(b => b.Barbers).FirstOrDefaultAsync(b => b.UserId == request.UserId);
            var barber = await _context.Barbers.FirstOrDefaultAsync(b => b.Id == request.BarberId);
            if (barber.BarberShopId != barberShop.Id)
            {
                throw new Exception("error");
            }
           
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
            //verifico se o usuario que enviou a requisicao eh o dono da barbearia, caso seja, pego o barbeiro com o id do barbeiro que passei na requisicao, depois verifico se o barbeiro pertence aq
            // a barbearia que eu passei, no primeiro momento eu so confiro a existencia e depois eu valido com um if, caso o id da barbearia do meu barbeiro
            // seja diferente do id do da barbearia do usuario eh enviado um not foun
            var barberShop = await _context.BarberShops.Include(b => b.Barbers).FirstOrDefaultAsync(b => b.UserId == request.UserId);
            var barber = await _context.Barbers.Include(b => b.Services).FirstOrDefaultAsync(b => b.Id == request.BarberId);

            if (barber.BarberShopId != barberShop.Id)
            {
                throw new NotFoundException(ResourceErrorMessages.NOT_FOUND_OBJECT);
            }

            var entityService = barber.Services.FirstOrDefault(s => s.Id == request.ServiceId);

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
