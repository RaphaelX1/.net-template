﻿using Architecture.Template.Application.Common.Exceptions;
using Architecture.Template.Domain.Entities;
using Architecture.Template.Domain.Interfaces.Repository;
using MediatR;

namespace Architecture.Template.Application.TodoList.Commands.DeleteTodoList;

public class DeleteTodoListCommand : IRequest
{
    public Guid Id { get; set; }
}

public class DeleteTodoListCommandHandler : IRequestHandler<DeleteTodoListCommand>
{
    private readonly ITodoListRepository todoListRepository;

    public DeleteTodoListCommandHandler(ITodoListRepository todoListRepository) =>
        this.todoListRepository = todoListRepository ?? throw new ArgumentNullException(nameof(todoListRepository));

    public async Task<Unit> Handle(DeleteTodoListCommand request, CancellationToken cancellationToken)
    {
        return await todoListRepository.DeleteAsync(l => l.Id.Equals(request.Id), cancellationToken)
            ? Unit.Value
            : throw new NotFoundException(nameof(TodoListEntity), request.Id);
    }
}