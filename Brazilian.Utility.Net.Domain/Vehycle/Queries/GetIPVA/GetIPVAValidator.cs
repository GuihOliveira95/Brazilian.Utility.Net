using FluentValidation;

namespace Brazilian.Utility.Net.Domain.Vehycle.Queries.GetIPVA
{
    public class GetIPVAValidator : AbstractValidator<GetIPVARequest>
    {
        public GetIPVAValidator()
        {
            RuleFor(request => request.LicensePlate)
                .NotNull().WithMessage("Vehycle license plate is required")
                .MaximumLength(7).MinimumLength(7).WithMessage("Vehycle license plate must have 7 characters")
                .Matches(@"^[a-zA-Z0-9]*$").WithMessage("Vehycle license plate must have only characters and numbers (A-Z and 0-9)");

        }
    }
}
