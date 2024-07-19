
namespace BarberShopApi.Application.Requests.Barber
{
    public class DeleteBarberRequest
    {
        public Guid BarberShopId { get; set; }
        public Guid BarberId { get; set; }
        public Guid UserId { get; set; }
    }
}
