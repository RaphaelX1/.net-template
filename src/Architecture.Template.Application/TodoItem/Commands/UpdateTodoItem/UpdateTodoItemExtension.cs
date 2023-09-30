using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.TodoItem.Commands.UpdateTodoItem;
internal static class UpdateTodoItemExtension
{
    internal static void UpdateEntityFieldsFrom(this TodoItemEntity todoItemEntity, UpdateTodoItemCommand updateTodoItemCommand)
    {
        todoItemEntity.Title = updateTodoItemCommand.Title;
        todoItemEntity.Done = updateTodoItemCommand.Done;
    }
}