using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;

namespace BarberShopApi.Domain.Entities
{
    public class Agendamento
    {
        public Guid Id { get; set; }
        public Guid BarberId { get; set; }
        public Barber Barber { get; set; }
        public Guid ServiceId { get; set; }
        public Service Service { get; set; }
        public DateTime Date { get; set; }
    }
}
