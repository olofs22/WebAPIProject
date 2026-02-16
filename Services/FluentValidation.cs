using FluentValidation;
using WebAPIProject.Models;

namespace WebAPIProject.Services
{
    public class CreateUserRequestValidator : AbstractValidator<Tournaments>
    {
        public CreateUserRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MinimumLength(3);

            RuleFor(x => x.StartDate)
                .GreaterThan(DateTime.Now);
        }
    }
}
