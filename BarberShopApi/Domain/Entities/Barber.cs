namespace BarberShopApi.Domain.Entities
{
    public class Barber
    {

        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int OpeningTime { get; set; }
        public int ClosingTime { get; set; }
        public Guid BarberShopId { get; set; }
        public BarberShop BarberShop { get; set; }
        public List<Service> Services { get; set; } = [];

    }
}
