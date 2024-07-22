using BarberShopApi.Domain.Entities;

namespace BarberShopApi.Application.Requests.Client
{
    public class ScheduleServiceRequest
    {
        public Guid ServiceId { get; set; }
        public Guid UserId { get; set; }
        public string Hour { get; set; }
        public DateTime Date { get; set; }
    }
}
