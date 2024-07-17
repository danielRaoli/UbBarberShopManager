using BarberShopApi.Application.Exceptions;
using FluentValidation;

namespace BarberShopApi.Application.Requests.Barber.CreatedBarber
{
    public class CreateBarberValidator : AbstractValidator<CreateBarberRequest>
    {
        public CreateBarberValidator()
        {
            RuleFor(request => request.Name).NotEmpty().WithMessage(ResourceErrorMessages.NAME_IS_EMPTY);
            RuleFor(request => request).Must(b => b.ClosingTime > b.OpeningTime).WithMessage(ResourceErrorMessages.INVALID_INTERVAL_HOURS);
        }
    }
}
