using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace BarberShopApi.Domain.Entities
{
    public class BarberShop
    {
        public Guid Id { get; set; } = new Guid();
        public Guid UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        public string Name { get; set; } = string.Empty;
        public IList<Barber> Barbers { get; set; } = [];
    }
}
