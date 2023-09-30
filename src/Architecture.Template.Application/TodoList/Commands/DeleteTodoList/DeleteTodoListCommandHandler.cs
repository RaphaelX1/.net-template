using Application.Common.Exceptions;
using Domain.Entities;
using Domain.Interfaces.Repository;
using MediatR;

namespace Application.TodoList.Commands.DeleteTodoList;

public class DeleteTodoListCommandHandler : IRequestHandler<DeleteTodoListCommand>
{
    private readonly ITodoListRepository _todoListRepository;

    public DeleteTodoListCommandHandler(ITodoListRepository todoListRepository) =>
        _todoListRepository = todoListRepository ?? throw new ArgumentNullException(nameof(todoListRepository));

    public async Task<Unit> Handle(DeleteTodoListCommand request, CancellationToken cancellationToken)
    {
        var entity = await _todoListRepository.SelectAsync(x => x.Id == request.Id, cancellationToken);

        Guard.Against.NotFound(request.Id, entity, nameof(entity.Id));

        await _todoListRepository.DeleteAsync(entity, cancellationToken);
        return Unit.Value;
    }
}
