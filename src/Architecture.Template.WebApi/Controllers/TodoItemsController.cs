using Architecture.Template.Application.TodoItem.Commands.CreateTodoItem;
using Architecture.Template.Application.TodoItem.Commands.DeleteTodoItem;
using Architecture.Template.Application.TodoItem.Commands.UpdateTodoItem;
using Architecture.Template.Application.TodoItem.Commands.UpdateTodoItemDetail;
using Microsoft.AspNetCore.Mvc;

namespace Architecture.Template.WebApi.Controllers;

//TODO: voltar quando fazer teste de autorização
//[Authorize]
public class TodoItemsController : ApiControllerBase
{
    //[HttpGet]//TODO: criar o pagination com o IQueryble component
    //public async Task<ActionResult<PaginatedList<TodoItemBriefDto>>> GetTodoItemsWithPagination([FromQuery] GetTodoItemsWithPaginationQuery query)
    //{
    //    return await Mediator.Send(query);
    //}

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateTodoItemCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, UpdateTodoItemCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpPut("[action]")]
    public async Task<ActionResult> UpdateItemDetails(Guid id, UpdateTodoItemDetailCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await Mediator.Send(new DeleteTodoItemCommand { Id = id });

        return NoContent();
    }
}
