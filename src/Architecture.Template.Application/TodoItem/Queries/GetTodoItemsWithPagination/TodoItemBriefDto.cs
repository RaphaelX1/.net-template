using Architecture.Template.Application.Common.Mappings;
using Architecture.Template.Domain.Entities;

namespace Architecture.Template.Application.TodoItem.Queries.GetTodoItemsWithPagination;

public class TodoItemBriefDto : IMapFrom<TodoItemEntity>
{
    public int Id { get; set; }

    public int ListId { get; set; }

    public string? Title { get; set; }

    public bool Done { get; set; }
}
