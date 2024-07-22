using BarberShopApi.Application.Exceptions;
using BarberShopApi.Application.Requests.Barber;
using BarberShopApi.Application.Requests.Barber.CreatedBarber;
using BarberShopApi.Application.Requests.Barber.EditBarber;
using BarberShopApi.Application.Requests.BarberShop;
using BarberShopApi.Application.Responses;
using BarberShopApi.Domain.Entities;
using BarberShopApi.Domain.Repositories;
using BarberShopApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BarberShopApi.Infrastructure.Repositories
{
    public class BarberShopRepository : IBarberShopRepository
    {
        private readonly AppDbContext _context;
        public BarberShopRepository(AppDbContext context)
        {
            _context = context;
        }



        public async Task<Response<BarberShop>> CreateBarberShop(CreateBarberShopRequest request)
        {
            var barberShop = await _context.BarberShops.FirstOrDefaultAsync(b => b.UserId == request.UserId);
            if (barberShop is not null)
            {
                throw new Exception("this account already has a registered barbershop");
            }

            barberShop = new BarberShop { Name = request.Name, UserId = request.UserId };
            _context.BarberShops.Add(barberShop);
            await _context.SaveChangesAsync();

            return new Response<BarberShop>(barberShop, 201, "Barber shop registered with success");
        }

        public async Task<Response<BarberShop>> GetMyBarber(GetMyBarberRequest request)
        {
            if (request.UserId == Guid.Empty)
            {
                throw new NotFoundException(ResourceErrorMessages.NOT_FOUND_OBJECT);
            }

            var barberShop = await _context.BarberShops.FirstOrDefaultAsync(b => b.UserId == request.UserId);

            return barberShop is null ? throw new NotFoundException(ResourceErrorMessages.NOT_FOUND_OBJECT) : new Response<BarberShop>(barberShop, 200);

        }

        public async Task<Response<BarberShop>> DeleteBarberShop(DeleteBarberShopRequest request)
        {
            var barberShopDb = await _context.BarberShops.FirstOrDefaultAsync(b => b.Id == request.BarberShopId && b.UserId == request.UserId);
            if (barberShopDb is null)
            {
                throw new NotFoundException(ResourceErrorMessages.NOT_FOUND_OBJECT);
            }

            _context.BarberShops.Remove(barberShopDb);
            await _context.SaveChangesAsync();

            return new Response<BarberShop>(barberShopDb, 204, "Barber Shop removed with success");
        }

        public async Task<Response<BarberShop>> UpdateBarberShop(UpdateBarberShopRequest request)
        {
            var barberShopDb = await _context.BarberShops.FirstOrDefaultAsync(b => b.Id == request.Id && b.UserId == request.UserId);
            if (barberShopDb is null)
            {
                throw new NotFoundException(ResourceErrorMessages.NOT_FOUND_OBJECT);
            }

            barberShopDb.Name = request.Name;
            _context.BarberShops.Update(barberShopDb);
            await _context.SaveChangesAsync();

            return new Response<BarberShop>(barberShopDb, 204, "Barber Shop Updated  with success");

        }


        public async Task<Response<Barber>> CreateBarber(CreateBarberRequest request)
        {
            var barberShop = await _context.BarberShops.FirstOrDefaultAsync(b => b.UserId == request.UserId && b.Id == request.BarberShopId);

            if (barberShop is null)
            {
                throw new NotFoundException(ResourceErrorMessages.NOT_FOUND_OBJECT);
            }
            var entityBarber = new Barber { Name = request.Name, ClosingTime = request.ClosingTime, OpeningTime = request.OpeningTime, BarberShopId = request.BarberShopId };

            _context.Barbers.Add(entityBarber);
            await _context.SaveChangesAsync();

            return new Response<Barber>(entityBarber, 201, "Barber registered with success");

        }

        public async Task<Response<Barber>> EditBarber(EditBarberRequest request)
        {
            var barberShopExists = await _context.BarberShops.FirstOrDefaultAsync(b => b.UserId == request.UserId && b.Id == request.BarberShopId);

            if(barberShopExists is null)
            {
                throw new NotFoundException(ResourceErrorMessages.NOT_FOUND_OBJECT);
            }

            var entityBarber = await _context.Barbers.FirstOrDefaultAsync(b => b.BarberShopId == request.BarberShopId && b.Id == request.BarberId);

            if (entityBarber is null) 
            { 
                throw new NotFoundException(ResourceErrorMessages.NOT_FOUND_OBJECT);
            }

            entityBarber.Name = request.Name;
            entityBarber.OpeningTime = request.OpeningTime;
            entityBarber.ClosingTime = request.ClosingTime; 

            _context.Barbers.Update(entityBarber);
            await _context.SaveChangesAsync();

            return new Response<Barber>(entityBarber, 204, "barber edited with success");
        }

        public async Task<Response<Barber>> DeleteBarber(DeleteBarberRequest request)
        {
            var barberShopExists = await _context.BarberShops.FirstOrDefaultAsync(b => b.UserId == request.UserId && b.Id == request.BarberShopId);

            if (barberShopExists is null)
            {
                throw new NotFoundException(ResourceErrorMessages.NOT_FOUND_OBJECT);
            }
            var entityBarber = await _context.Barbers.FirstOrDefaultAsync(b => b.BarberShopId == request.BarberShopId && b.Id == request.BarberId);

            if(entityBarber is null)
            {
                throw new NotFoundException(ResourceErrorMessages.NOT_FOUND_OBJECT);
            }

            _context.Barbers.Remove(entityBarber);
            await _context.SaveChangesAsync();

            return new Response<Barber>(entityBarber, 204, "Barber removed with success");
        }


    }
}
