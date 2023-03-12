using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Architecture.Template.Domain.Entities;

public class TodoItemEntity : AuditableEntity
{
    public Guid ListId { get; set; }

    public string? Title { get; set; }

    public string? Note { get; set; }

    public PriorityLevel Priority { get; set; }

    public DateTime? Reminder { get; set; }

    //public TodoListEntity List { get; set; } = null!;
}
