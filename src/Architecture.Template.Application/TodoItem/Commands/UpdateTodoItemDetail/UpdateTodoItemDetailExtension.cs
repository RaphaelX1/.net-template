using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using MediatR;

namespace Application.TodoItem.Commands.UpdateTodoItemDetail;
internal static class UpdateTodoItemDetailExtension
{
    internal static void UpdateDetailEntityFieldsFrom(this TodoItemEntity todoItemEntity, UpdateTodoItemDetailCommand updateTodoItemDetailCommand)
    {
        todoItemEntity.ListId = updateTodoItemDetailCommand.ListId;
        todoItemEntity.Priority = updateTodoItemDetailCommand.Priority;
        todoItemEntity.Note = updateTodoItemDetailCommand.Note;
    }
}
