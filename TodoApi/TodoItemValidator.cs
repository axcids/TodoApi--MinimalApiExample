using FluentValidation;
using TodoApi;

namespace TodoApi; 
public class TodoItemValidator : AbstractValidator<TodoItem> {
    public TodoItemValidator() {
        RuleSet("Create", () => {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required.")
                .MaximumLength(100)
                .WithMessage("Name must be less than 100 characters.");
            RuleFor(x => x.IsComplete)
                .NotNull().WithMessage("IsComplete is required in creation new item.")
                .Must(x =>  x == false).WithMessage("IsComplete value must be false in creation new item.");
        });
    }
}