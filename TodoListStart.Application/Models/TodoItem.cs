using TodoListStart.Application.ValueObjects;
using TodoListStart.Application.Interfaces;

namespace TodoListStart.Application.Models
{
    public partial class TodoItem : TodoItemValue, IDateTimeAudit, IEntityIdentity
    {
        public TodoList TodoList { get; set; }
    }
}
