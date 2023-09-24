using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hdn.Core.Architecture.Domain.Common;

namespace Hdn.Core.Architecture.Domain.Events;
public class TodoItemDeletedEvent: BaseEvent
{
    public TodoItemEntity TodoItem { get; }
    public TodoItemDeletedEvent(TodoItemEntity todoItem) =>
        TodoItem = todoItem ?? throw new ArgumentNullException(nameof(todoItem));
}
