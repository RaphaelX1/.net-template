using Architecture.Template.Application.TodoList.Queries.GetTodos;
using Architecture.Template.Domain.Enums;
using Architecture.Template.Domain.Interfaces.Repository;
using AutoMapper;
using MediatR;

namespace Architecture.Template.Application.TodoList.Handlers;
public class GetTodosQueryHandler : IRequestHandler<GetTodoQuery, TodosVm>
{
    private readonly ITodoListRepository todoListRepository;
    private readonly IMapper mapper;

    public GetTodosQueryHandler(ITodoListRepository todoListRepository, IMapper mapper)
    {
        this.todoListRepository = todoListRepository ?? throw new ArgumentNullException(nameof(todoListRepository));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<TodosVm> Handle(GetTodoQuery request, CancellationToken cancellationToken)
    {
        return new TodosVm
        {
            PriorityLevels = Enum.GetValues(typeof(PriorityLevel))
                .Cast<PriorityLevel>()
                .Select(p => new PriorityLevelDto { Value = (int)p, Name = p.ToString() })
                .ToList(),

            Lists = mapper.Map<IList<TodoListDto>>(await todoListRepository.SelectAllAsync(cancellationToken: cancellationToken))
        };
    }
}
