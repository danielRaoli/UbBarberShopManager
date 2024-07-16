using BarberShopApi.Application.Exceptions;
using BarberShopApi.Application.Requests.Barber;
using FluentValidation;

namespace BarberShopApi.Application.Requests.BarberShop
{
    public class CreaterBarberShopValidator : AbstractValidator<CreateBarberShopRequest>
    {
        public CreaterBarberShopValidator()
        {
            RuleFor(request => request.Name).NotEmpty().WithMessage(ResourceErrorMessages.NAME_IS_EMPTY);
        }
    }
}
