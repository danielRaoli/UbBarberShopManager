using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;

namespace BarberShopApi.Domain.Entities
{
    public class Schedule
    {
        public Guid Id { get; set; }
        public Guid ServiceId { get; set; }
        public Service Service { get; set; }
        public Guid UserId  { get; set; }
        public User User { get; set; }
        public DateTime Date { get; set; }
    }
}
