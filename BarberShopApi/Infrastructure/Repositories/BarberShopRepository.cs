using BarberShopApi.Application.Exceptions;
using BarberShopApi.Application.Requests.Barber;
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
            var entity = new BarberShop { Name = request.Name };
            _context.BarberShops.Add(entity);
            await _context.SaveChangesAsync();

            return new Response<BarberShop>(entity, 201, "Barber shop registered with success");
        }

        public async Task<Response<BarberShop>> DeleteBarberShop(DeleteBarberShopRequest request)
        {
            var barberShopDb = await _context.BarberShops.FirstOrDefaultAsync(b => b.Id == request.BarberShopId);
            if (barberShopDb is null)
            {
                throw new NotFoundException(ResourceErrorMessages.NOT_FOUND_OBJECT);
            }

            _context.BarberShops.Remove(barberShopDb);
            await _context.SaveChangesAsync();

            return new Response<BarberShop>(barberShopDb, 204, "Barber Shop removed with success");
        }

        public async Task<Response<List<BarberShop>>> GetAllBarberShops()
        {
            var barberShops = await _context.BarberShops.ToListAsync();
            return new Response<List<BarberShop>>(barberShops, 200);
        }

        public async Task<Response<BarberShop>> GetBarberShop(GetBarberShopRequest request)
        {
            var barberShopDb = await _context.BarberShops.Include(b => b.Barbers).FirstOrDefaultAsync(b => b.Id == request.Id);
            if (barberShopDb is null)
            {
                throw new NotFoundException(ResourceErrorMessages.NOT_FOUND_OBJECT);
            }

            return new Response<BarberShop>(barberShopDb, 200);

        }

        public async Task<Response<BarberShop>> UpdateBarberShop(UpdateBarberShopRequest request)
        {
            var barberShopDb = await _context.BarberShops.FirstOrDefaultAsync(b => b.Id == request.Id);
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
            var barberShop = await _context.BarberShops.FirstOrDefaultAsync(b => b.Id == request.BarberShopId);

            if (barberShop is null)
            {
                throw new NotFoundException(ResourceErrorMessages.NOT_FOUND_OBJECT);
            }
            var entityBarber = new Barber { Name = request.Name, ClosingTime = request.ClosingTime, OpeningTime = request.OpeningTime, BarberShopId = request.BarberShopId };

            _context.Barbers.Add(entityBarber);
            await _context.SaveChangesAsync();

            return new Response<Barber>(entityBarber, 201, "Barber registered with success");

        }
    }
}
