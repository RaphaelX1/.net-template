using System;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Hdn.Core.Architecture.Application.TodoItem.Commands.CreateTodoItem;
using Hdn.Core.Architecture.Application.TodoItem.Handlers;
using Hdn.Core.Architecture.Domain.Entities;
using Hdn.Core.Architecture.Domain.Interfaces.Repository;
using Moq;
using Xunit;

namespace Hdn.Core.Architecture.Application.Tests.TodoItem.Handlers;

[Collection("Testing TodoItemCommand")]
public class CreateTodoItemCommandHandlerTest
{
    public CreateTodoItemCommandHandlerTest()
    {

    }

    [Fact(DisplayName = "Dado que o objeto CreateTodoItemCommand � valido, quando o metodo create � chamado, ent�o dever� retornar TodoItemEntity.Id valido")]
    public async Task Should_Return_Success_ToDoItemHandler_Create()
    {
        //Arrange
        var fixture = new Fixture();

        fixture.Customize<CreateTodoItemCommand>(c =>
            c.With(p => p.ListId, Guid.Parse("2f02ffb6-1769-4ef1-80d5-07fe4b64db15"))
            .With(p => p.Title, "teste"));

        var request = fixture.Create<CreateTodoItemCommand>();

        fixture.Customize<TodoItemEntity>(c => 
            c.With(p => p.ListId, Guid.Parse("2f02ffb6-1769-4ef1-80d5-07fe4b64db15"))
            .With(p => p.Title, "teste")
            .With(p => p.Id, Guid.Parse("fe87aa6d-4ec8-4224-af21-59d8acffdf60")));

        var entityToCreate = fixture.Create<TodoItemEntity>();

        var todoItemRepository = new Mock<ITodoItemRepository>();

        var cancelationToken = new CancellationToken();

        todoItemRepository.Setup(x => x.InsertAsync(It.IsAny<TodoItemEntity>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(entityToCreate);

        var commandHandler = new CreateTodoItemCommandHandler(todoItemRepository.Object);

        //Act
        var entityResponse = await commandHandler.Handle(request, cancelationToken);

        //Assert
        entityToCreate.Title.Should().Be(request.Title);
        entityToCreate.ListId.Should().Be(request.ListId);
        entityToCreate.Id.Should().Be(entityResponse);
        todoItemRepository.Verify(x => x.InsertAsync(It.IsAny<TodoItemEntity>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact(DisplayName ="Dado que o objeto CreateTodoItemCommand � invalido, quando o metodo create � chamado, ent�o dever� retorar TodoItemEntity.Id invalido")]
    public async Task Should_Fail_ToDoItemHandler_Create()
    {
        //Arrange
        var fixture = new Fixture();

        fixture.Customize<CreateTodoItemCommand>(c =>
            c.With(p => p.ListId, Guid.Empty)
            .With(p => p.Title, "testeFail"));

        var request = fixture.Create<CreateTodoItemCommand>();

        fixture.Customize<TodoItemEntity>(c =>
            c.With(p => p.ListId, Guid.Parse("fe87aa6d-4ec8-4224-af21-59d8acffdf60"))
            .With(p => p.Title, "teste")
            .With(p => p.Id, Guid.Empty).With(p => p.List, new TodoListEntity()));

        var entityToCreate = fixture.Create<TodoItemEntity>();

        var todoItemRepository = new Mock<ITodoItemRepository>();

        var cancelationToken = new CancellationToken();

        todoItemRepository.Setup(x => x.InsertAsync(It.IsAny<TodoItemEntity>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(entityToCreate);

        var commandHandler = new CreateTodoItemCommandHandler(todoItemRepository.Object);

        //Act
        var entityResponse = await commandHandler.Handle(request, cancelationToken);

        //Assert
        entityResponse.Should().BeEmpty();
        entityToCreate.ListId.Should().NotBe(request.ListId);
        entityToCreate.Title.Should().NotBe(request.Title);
        todoItemRepository.Verify(x => x.InsertAsync(It.IsAny<TodoItemEntity>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}


