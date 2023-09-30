using Application.Common.Exceptions;
using Domain.Entities;
using Domain.Interfaces.Repository;
using MediatR;

namespace Application.TodoList.Commands.DeleteTodoList;

public record class DeleteTodoListCommand(Guid Id) : IRequest;