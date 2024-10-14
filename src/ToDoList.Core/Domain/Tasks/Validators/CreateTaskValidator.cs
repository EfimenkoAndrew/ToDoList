using FluentValidation;
using ToDoList.Core.Domain.Tasks.Data;

namespace ToDoList.Core.Domain.Tasks.Validators;

public class CreateTaskValidator : AbstractValidator<CreateTaskData>
{
    public CreateTaskValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MinimumLength(1)
            .MaximumLength(255);

        RuleFor(x => x.Description)
            .MaximumLength(500);
    }
}
