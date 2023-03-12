using Architecture.Template.Domain.Entities;
using Architecture.Template.Domain.Interfaces.Repository;
using Architecture.Template.Infrastructure.Common;
using Architecture.Template.Infrastructure.Context;

namespace Architecture.Template.Infrastructure.Repository;

internal class TodoItemRepository : BaseRepository<TodoItemEntity>, ITodoItemRepository
{
    public TodoItemRepository(ApplicationDbContext context) : base(context)
    {

    }
}
