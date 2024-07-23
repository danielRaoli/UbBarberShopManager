namespace BarberShopApi.Application.Requests.BarberShop
{
    public class GetBarberHistoryScheduleRequest
    {
        public Guid UserId { get; set; }
        public Guid BarberId { get; set; }
    }
}
