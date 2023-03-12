using Architecture.Template.Application.Common.Exceptions;
using Architecture.Template.Application.TodoItem.Commands.UpdateTodoItem;
using Architecture.Template.Domain.Entities;
using Architecture.Template.Domain.Interfaces.Repository;
using MediatR;

namespace Architecture.Template.Application.TodoItem.Handlers;
public class UpdateTodoItemCommandHandler : IRequestHandler<UpdateTodoItemCommand>
{
    private readonly ITodoItemRepository todoItemRepository;

    public UpdateTodoItemCommandHandler(ITodoItemRepository todoItemRepository) =>
    this.todoItemRepository = todoItemRepository ?? throw new ArgumentNullException(nameof(todoItemRepository));

    public async Task<Unit> Handle(UpdateTodoItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await todoItemRepository.SelectAsync(l => l.Id.Equals(request.Id), cancellationToken);

        if (entity == null)
            throw new NotFoundException(nameof(TodoListEntity), request.Id);

        entity.Title = request.Title;

        await todoItemRepository.UpdateAsync(entity, cancellationToken);

        return Unit.Value;
    }
}
