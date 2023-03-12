using MediatR;

namespace Architecture.Template.Application.TodoList.Commands.CreateTodoList;

public class CreateTodoListCommand : IRequest<Guid>
{
    public string? Title { get; set; }
}
