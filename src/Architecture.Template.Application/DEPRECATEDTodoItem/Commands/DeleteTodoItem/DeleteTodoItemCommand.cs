using MediatR;

namespace Architecture.Template.Application.TodoItem.Commands.DeleteTodoItem;

public class DeleteTodoItemCommand : IRequest
{
    public Guid Id { get; set; }
}
