using Architecture.Template.Application.Common.Exceptions;
using Architecture.Template.Application.TodoList.Commands.UpdateTodoList;
using Architecture.Template.Domain.Entities;
using Architecture.Template.Domain.Interfaces.Repository;
using MediatR;

namespace Architecture.Template.Application.TodoList.Handlers;

public class UpdateTodoListCommandHandler : IRequestHandler<UpdateTodoListCommand>
{
    private readonly ITodoListRepository todoListRepository;

    public UpdateTodoListCommandHandler(ITodoListRepository todoListRepository) =>
        this.todoListRepository = todoListRepository ?? throw new ArgumentNullException(nameof(todoListRepository));

    public async Task<Unit> Handle(UpdateTodoListCommand request, CancellationToken cancellationToken)
    {
        var entity = await todoListRepository.SelectAsync(l => l.Id.Equals(request.Id), cancellationToken);

        if (entity == null)
            throw new NotFoundException(nameof(TodoListEntity), request.Id);

        entity.Title = request.Title;

        await todoListRepository.UpdateAsync(entity, cancellationToken);

        return Unit.Value;
    }
}
