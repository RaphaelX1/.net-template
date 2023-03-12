using MediatR;

namespace Architecture.Template.Application.TodoList.Queries.GetTodos;

public class GetTodosQuery : IRequest<TodosVm>
{
}
