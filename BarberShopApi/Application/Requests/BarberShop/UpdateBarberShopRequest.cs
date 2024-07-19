using BarberShopApi.Application.Exceptions;
using BarberShopApi.Application.Requests.BarberShop;

namespace BarberShopApi.Application.Requests.Barber
{
    public class UpdateBarberShopRequest
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; } = string.Empty;

        public void Validate()
        {
            var validator = new UpdateBarberShopValidator();
            var result = validator.Validate(this);

            if(result.IsValid is false)
            {
                throw new OnValidateException(result.Errors.Select(e => e.ErrorMessage).ToList());
            }
        }
    }
}
