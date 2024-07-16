namespace BarberShopApi.Domain.Entities
{
    public class Service
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid BarberId { get; set; }
        public Barber Baber { get; set; }
        public decimal Price { get; set; }


    }
}
