using Architecture.Template.Application.TodoList.Commands.CreateTodoList;
using Architecture.Template.Domain.Entities;
using Architecture.Template.Domain.Interfaces.Repository;
using MediatR;

namespace Architecture.Template.Application.TodoList.Handlers;
public class CreateTodoListCommandHandler : IRequestHandler<CreateTodoListCommand, Guid>
{
    private readonly ITodoListRepository _todoListRepository;

    public CreateTodoListCommandHandler(ITodoListRepository todoListRepository) =>
        this._todoListRepository = todoListRepository ?? throw new ArgumentNullException(nameof(todoListRepository));

    public async Task<Guid> Handle(CreateTodoListCommand request, CancellationToken cancellationToken)
    {
        var entity = new TodoListEntity
        {
            Title = request.Title
        };

        await _todoListRepository.InsertAsync(entity, cancellationToken);

        return entity.Id;
    }
}
