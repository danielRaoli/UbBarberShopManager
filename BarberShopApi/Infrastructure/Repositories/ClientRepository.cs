using BarberShopApi.Application.Exceptions;
using BarberShopApi.Application.Requests.Barber;
using BarberShopApi.Application.Requests.Client;
using BarberShopApi.Application.Responses;
using BarberShopApi.Domain.Entities;
using BarberShopApi.Domain.Repositories;
using BarberShopApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BarberShopApi.Infrastructure.Repositories
{
    public class ClientRepository(AppDbContext context) : IClientRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<Response<List<BarberShop>>> GetAllBarberShops()
        {

            var barberShops = await _context.BarberShops.ToListAsync();
            return new Response<List<BarberShop>>(barberShops, 200);
        }

        public async Task<Response<BarberShop>> GetBarberShop(GetBarberShopRequest request)
        {
            var barberShopDb = await _context.BarberShops.Include(b => b.Barbers).ThenInclude(b => b.Services).FirstOrDefaultAsync(b => b.Id == request.Id);
            if (barberShopDb is null)
            {
                throw new NotFoundException(ResourceErrorMessages.NOT_FOUND_OBJECT);
            }

            return new Response<BarberShop>(barberShopDb, 200);

        }

        public async Task<Response<List<Schedule>>> GetUserScheduleHistory(GetUserScheduleHistory request)
        {
            var schedules = await _context.Schedules.Where(s => s.UserId ==  request.UserId).ToListAsync();
            return new Response<List<Schedule>>(schedules, 200);
        }

        public async Task<Response<Schedule>> Schedule(ScheduleServiceRequest request)
        {

            var service = await _context.Services.FirstOrDefaultAsync(s => s.Id == request.ServiceId);
            if(service is null) throw new NotFoundException(ResourceErrorMessages.NOT_FOUND_OBJECT);

            var date = new DateTime(year: request.Date.Year, month: request.Date.Month, day: request.Date.Day, hour: 0, minute: 0,0); 
          
            var hour = TimeSpan.Parse(request.Hour);
            var completeDate = date.Add(hour);

            var scheduleEntity = new Schedule { ServiceId = request.ServiceId, UserId = request.UserId, Date = completeDate };

            _context.Schedules.Add(scheduleEntity);
            await _context.SaveChangesAsync();  

            return new Response<Schedule>(scheduleEntity, 200);
        }
    }
}
