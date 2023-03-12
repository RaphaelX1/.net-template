using Architecture.Template.Application.Common.Exceptions;
using Architecture.Template.Application.TodoItem.Commands.UpdateTodoItemDetail;
using Architecture.Template.Domain.Entities;
using Architecture.Template.Domain.Interfaces.Repository;
using MediatR;

namespace Architecture.Template.Application.TodoItem.Handlers;
public class UpdateTodoItemDetailCommandHandler : IRequestHandler<UpdateTodoItemDetailCommand>
{
    private readonly ITodoItemRepository todoItemRepository;

    public UpdateTodoItemDetailCommandHandler(ITodoItemRepository todoItemRepository) =>
        this.todoItemRepository = todoItemRepository ?? throw new ArgumentNullException(nameof(todoItemRepository));

    public async Task<Unit> Handle(UpdateTodoItemDetailCommand request, CancellationToken cancellationToken)
    {
        var entity = await todoItemRepository.SelectAsync(l => l.Id.Equals(request.Id), cancellationToken);

        if (entity == null)
            throw new NotFoundException(nameof(TodoItemEntity), request.Id);

        entity.ListId = request.ListId;
        entity.Priority = request.Priority;
        entity.Note = request.Note;

        await todoItemRepository.UpdateAsync(entity, cancellationToken);

        return Unit.Value;
    }
}
