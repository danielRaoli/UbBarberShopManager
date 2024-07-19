using System.Text.Json.Serialization;

namespace BarberShopApi.Domain.Entities
{
    public class Service
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid BarberId { get; set; }
        [JsonIgnore]
        public Barber Baber { get; set; }
        public double Price { get; set; }


    }
}
