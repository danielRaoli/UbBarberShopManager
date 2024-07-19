namespace BarberShopApi.Application.Requests.Barber
{
    public class DeleteBarberShopRequest
    {
        public Guid BarberShopId { get; set; }
        public Guid UserId { get; set; }
    }
}
