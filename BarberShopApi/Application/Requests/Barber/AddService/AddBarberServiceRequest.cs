using BarberShopApi.Application.Exceptions;
using System.Text.Json.Serialization;

namespace BarberShopApi.Application.Requests.Barber.AddService
{
    public class AddBarberServiceRequest
    {
        [JsonIgnore]
        public Guid BarberId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }  


        public void Validate()
        {
            var validator = new AddBarberServiceValidator();
            var result = validator.Validate(this);

            if (result.IsValid is false)
            {
                var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new OnValidateException(errors);
            }
        }
    }
}
