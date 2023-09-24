using Architecture.Template.Domain.Interfaces.Repository;
using FluentValidation;

namespace Architecture.Template.Application.TodoList.Commands.UpdateTodoList;

public class UpdateTodoListCommandValidator : AbstractValidator<UpdateTodoListCommand>
{
    private readonly ITodoListRepository todoListRepository;

    public UpdateTodoListCommandValidator(ITodoListRepository todoListRepository)
    {
        this.todoListRepository = todoListRepository ?? throw new ArgumentNullException(nameof(todoListRepository));

        ApplyRules();
    }
    private void ApplyRules()
    {
        RuleFor(v => v.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.")
            .MustAsync(BeUniqueTitle).WithMessage("The specified title already exists.");
    }

    public async Task<bool> BeUniqueTitle(UpdateTodoListCommand model, string title, CancellationToken cancellationToken)
    {
        var listCollection = await todoListRepository.SelectAllAsync(l => l.Id != model.Id, cancellationToken);
        return listCollection.ToList().All(l => l.Title != title);//TODO: melhorar isso aqui usando o Exist
    }
}
