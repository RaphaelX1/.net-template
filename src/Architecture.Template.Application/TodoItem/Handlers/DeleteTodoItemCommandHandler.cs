using Architecture.Template.Application.Common.Exceptions;
using Architecture.Template.Application.TodoItem.Commands.DeleteTodoItem;
using Architecture.Template.Domain.Entities;
using Architecture.Template.Domain.Interfaces.Repository;
using MediatR;

namespace Architecture.Template.Application.TodoItem.Handlers;

public class DeleteTodoItemCommandHandler : IRequestHandler<DeleteTodoItemCommand>
{
    private readonly ITodoItemRepository todoItemRepository;

    public DeleteTodoItemCommandHandler(ITodoItemRepository todoItemRepository) =>
     this.todoItemRepository = todoItemRepository ?? throw new ArgumentNullException(nameof(todoItemRepository));

    public async Task<Unit> Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
    {
        return await todoItemRepository.DeleteAsync(l => l.Id.Equals(request.Id), cancellationToken)
            ? Unit.Value
            : throw new NotFoundException(nameof(TodoItemEntity), request.Id);
    }
}
