namespace BarberShopApi.Domain.Entities
{
    public class Agendamento
    {
        public Guid Id { get; set; }
        public Guid BarberId { get; set; }
        public Guid ClientId { get; set; }
        public Guid ServiceId { get; set; }
        public DateTime Date { get; set; }
    }
}
