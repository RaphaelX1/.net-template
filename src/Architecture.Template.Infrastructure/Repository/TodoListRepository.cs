using Architecture.Template.Domain.Entities;
using Architecture.Template.Domain.Interfaces.Repository;
using Architecture.Template.Infrastructure.Common;
using Architecture.Template.Infrastructure.Context;

namespace Architecture.Template.Infrastructure.Repository;

internal class TodoListRepository : BaseRepository<TodoListEntity>, ITodoListRepository
{
    public TodoListRepository(ApplicationDbContext context) : base(context)
    {

    }
}
