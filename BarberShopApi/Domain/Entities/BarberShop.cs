namespace BarberShopApi.Domain.Entities
{
    public class BarberShop
    {
        public Guid Id { get; set; } = new Guid();
        public string Name { get; set; } = string.Empty;
        public IList<Barber> Barbers { get; set; } = [];
    }
}
