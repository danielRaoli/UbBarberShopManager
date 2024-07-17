using BarberShopApi.Application.Exceptions;
using FluentValidation;

namespace BarberShopApi.Application.Requests.Barber.AddService
{
    public class AddBarberServiceValidator : AbstractValidator<AddBarberServiceRequest>
    {
        public AddBarberServiceValidator()
        {
            RuleFor(request => request.Name).NotEmpty().WithMessage(ResourceErrorMessages.NAME_IS_EMPTY);
            RuleFor(request => request.Price).GreaterThanOrEqualTo(5).WithMessage(ResourceErrorMessages.INVALID_PRICE);
        }
    }
}
