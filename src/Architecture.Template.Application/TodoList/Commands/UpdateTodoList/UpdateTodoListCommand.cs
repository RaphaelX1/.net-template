using MediatR;

namespace Architecture.Template.Application.TodoList.Commands.UpdateTodoList;

public class UpdateTodoListCommand : IRequest
{
    public Guid Id { get; set; }

    public string? Title { get; set; }
}
