using Architecture.Template.Application.TodoList.Commands.PurgeTodoList;
using Architecture.Template.Domain.Interfaces.Repository;
using MediatR;

namespace Architecture.Template.Application.TodoList.Handlers;

public class PurgeTodoListCommandHandler : IRequestHandler<PurgeTodoListCommand>
{
    private readonly ITodoListRepository _todoListRepository;

    public PurgeTodoListCommandHandler(ITodoListRepository todoListRepository)
    {
        this._todoListRepository = todoListRepository ?? throw new ArgumentNullException(nameof(todoListRepository));
    }

    public async Task<Unit> Handle(PurgeTodoListCommand request, CancellationToken cancellationToken)
    {
        await _todoListRepository.DeleteRangeAsync(await _todoListRepository.SelectAllAsync(cancellationToken: cancellationToken));
        return Unit.Value;//TODO: criar um DeleteAll e um SelectRange
    }
}
