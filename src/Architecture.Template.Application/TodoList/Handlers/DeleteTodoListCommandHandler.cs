using Architecture.Template.Application.Common.Exceptions;
using Architecture.Template.Application.TodoList.Commands.DeleteTodoList;
using Architecture.Template.Domain.Entities;
using Architecture.Template.Domain.Interfaces.Repository;
using MediatR;

namespace Architecture.Template.Application.TodoList.Handlers;

public class DeleteTodoListCommandHandler : IRequestHandler<DeleteTodoListCommand>
{
    private readonly ITodoListRepository _todoListRepository;

    public DeleteTodoListCommandHandler(ITodoListRepository todoListRepository) =>
        this._todoListRepository = todoListRepository ?? throw new ArgumentNullException(nameof(todoListRepository));

    public async Task<Unit> Handle(DeleteTodoListCommand request, CancellationToken cancellationToken)
    {
        return await _todoListRepository.DeleteAsync(l => l.Id.Equals(request.Id), cancellationToken)
            ? Unit.Value
            : throw new NotFoundException(nameof(TodoListEntity), request.Id);
    }
}
