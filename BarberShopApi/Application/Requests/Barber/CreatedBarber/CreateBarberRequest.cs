using BarberShopApi.Application.Exceptions;
using BarberShopApi.Domain.Entities;
using System.Text.Json.Serialization;

namespace BarberShopApi.Application.Requests.Barber.CreatedBarber
{
    public class CreateBarberRequest
    {
        [JsonIgnore]
        public Guid BarberShopId { get; set; }

        public string Name { get; set; } = string.Empty;
        public int OpeningTime { get; set; }
        public int ClosingTime { get; set; }


        public void Validate()
        {
            var validator = new CreateBarberValidator();
            var result = validator.Validate(this);

            if (result.IsValid is false)
            {
                var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new OnValidateException(errors);
            }
        }

    }
}
