namespace BarberShopApi.Application.Requests.Barber
{
    public class DeleteBarberServiceRequest
    {
        public Guid BarberId { get; set; }
        public Guid ServiceId { get; set; }
    }
}
