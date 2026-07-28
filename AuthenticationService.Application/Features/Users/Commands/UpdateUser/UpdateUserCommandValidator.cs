using FluentValidation;

namespace AuthenticationService.CrossCutting.Users.Commands.UpdateUser;

public class UpdateUserCommandValidator
    : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Phone)
            .NotEmpty()
            .MaximumLength(15);

        RuleFor(x => x.Status)
            .InclusiveBetween((byte)0, (byte)1);
    }
}