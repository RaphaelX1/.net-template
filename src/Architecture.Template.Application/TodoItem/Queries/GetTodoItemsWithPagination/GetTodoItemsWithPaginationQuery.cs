using MediatR;

namespace Architecture.Template.Application.TodoItem.Queries.GetTodoItemsWithPagination;

//TODO: adicionar paginacao com o IQueryble package
public class GetTodoItemsWithPaginationQuery : IRequest<TodoItemBriefDto>
{
    public int ListId { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
