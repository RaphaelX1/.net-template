using Architecture.Template.Domain.Enums;
using MediatR;

namespace Architecture.Template.Application.TodoItem.Commands.UpdateTodoItemDetail;

public class UpdateTodoItemDetailCommand : IRequest
{
    public Guid Id { get; set; }

    public Guid ListId { get; set; }

    public PriorityLevel Priority { get; set; }

    public string? Note { get; set; }
}
