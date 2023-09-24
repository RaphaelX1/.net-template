﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Hdn.Core.Architecture.Domain.Events;

namespace Architecture.Template.Domain.Entities;

public class TodoItemEntity : BaseAuditableEntity
{

    public Guid ListId { get; set; }

    public string? Title { get; set; }

    public string? Note { get; set; }

    public PriorityLevel Priority { get; set; }

    public DateTime? Reminder { get; set; }


    private bool _done;
    public bool Done
    {
        get => _done; set
        {

            if (value && !_done)
                AddDomainEvent(new TodoItemCompletedEvent(this));
        }
    }

    public TodoListEntity List { get; set; } = null!;
}
