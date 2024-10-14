using FluentValidation;
using ToDoList.Core.Domain.Users.Data;

namespace ToDoList.Core.Domain.Users.Validators;

public class CreateUserValidator : AbstractValidator<CreateUserData>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(250);

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MinimumLength(6)
            .MaximumLength(250);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MinimumLength(6)
            .MaximumLength(250);
    }
}
