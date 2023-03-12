using MediatR;

namespace Architecture.Template.Application.TodoList.Queries.GetTodos;

public class GetTodoQuery : IRequest<TodosVm>
{
}
