using Architecture.Template.Application.TodoList.Commands.PurgeTodoLists;
using Architecture.Template.Domain.Interfaces.Repository;
using MediatR;

namespace Architecture.Template.Application.TodoList.Handlers;

public class PurgeTodoListsCommandHandler : IRequestHandler<PurgeTodoListsCommand>
{
    private readonly ITodoListRepository todoListRepository;

    public PurgeTodoListsCommandHandler(ITodoListRepository todoListRepository)
    {
        this.todoListRepository = todoListRepository ?? throw new ArgumentNullException(nameof(todoListRepository));
    }

    public async Task<Unit> Handle(PurgeTodoListsCommand request, CancellationToken cancellationToken)
    {
        await todoListRepository.DeleteRangeAsync(await todoListRepository.SelectAllAsync(cancellationToken: cancellationToken));
        return Unit.Value;//TODO: criar um DeleteAll e um SelectRange
    }
}
