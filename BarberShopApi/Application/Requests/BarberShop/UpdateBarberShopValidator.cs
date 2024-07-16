using BarberShopApi.Application.Exceptions;
using BarberShopApi.Application.Requests.Barber;
using FluentValidation;

namespace BarberShopApi.Application.Requests.BarberShop
{
    public class UpdateBarberShopValidator : AbstractValidator<UpdateBarberShopRequest>
    {
        public UpdateBarberShopValidator()
        {
            RuleFor(request => request.Name).NotEmpty().WithMessage(ResourceErrorMessages.NAME_IS_EMPTY);
        }
    }
}
