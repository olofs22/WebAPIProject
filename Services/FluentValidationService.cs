using FluentValidation;
using WebAPIProject.DTO.GamesDTOs;
using WebAPIProject.DTO.TournamentDTOs;
using WebAPIProject.Models;

namespace WebAPIProject.Services
{
    public class CreateTournamentRequestValidator : AbstractValidator<TournamentCreateDTO> //fluentvalidation class that sets rules for different attributes
    {
        public CreateTournamentRequestValidator() //validator for creating a tournament, Title has to be minimum 3 characters and startdate has to be in the future, cant be past time
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MinimumLength(3);

            RuleFor(x => x.StartDate)
                .GreaterThan(_ => DateTime.UtcNow);
        }
    }

    public class CreateGameRequestValidator : AbstractValidator<GameCreateDTO> //validator for creating a game, Title has to be minimum 3 characters and startdate has to be in the future, cant be past time
    {
        public CreateGameRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MinimumLength(3);
            RuleFor(x => x.StartTime)
                .GreaterThan(_ => DateTime.UtcNow);
        }
    }
}
