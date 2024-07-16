using BarberShopApi.Application.Exceptions;
using BarberShopApi.Application.Requests.BarberShop;

namespace BarberShopApi.Application.Requests.Barber
{
    public class CreateBarberShopRequest
    {
        public string Name { get; set; } = string.Empty;

        public void Validate()
        {
            var validator = new CreaterBarberShopValidator();
            var result = validator.Validate(this);

            if (result.IsValid is false)
            {

                var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new OnValidateException(errors);

            }
        }
    }
}
