using Architecture.Template.Application.TodoList.Commands.CreateTodoList;
using Architecture.Template.Application.TodoList.Commands.DeleteTodoList;
using Architecture.Template.Application.TodoList.Commands.UpdateTodoList;
using Architecture.Template.Application.TodoList.Queries.GetTodos;
using Microsoft.AspNetCore.Mvc;

namespace Architecture.Template.WebApi.Controllers;

//TODO: voltar quando fazer teste de autorização
//[Authorize]
public class TodoListsController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<TodosVm>> Get()
    {
        return await Mediator.Send(new GetTodosQuery());
    }

    //[HttpGet("{id}")] //TODO: criar o export CSV
    //public async Task<FileResult> Get(int id)
    //{
    //    var vm = await Mediator.Send(new ExportTodosQuery { ListId = id });

    //    return File(vm.Content, vm.ContentType, vm.FileName);
    //}

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateTodoListCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, UpdateTodoListCommand command)
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
        await Mediator.Send(new DeleteTodoListCommand { Id = id });

        return NoContent();
    }
}
