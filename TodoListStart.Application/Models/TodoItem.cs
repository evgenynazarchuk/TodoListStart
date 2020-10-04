using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoListStart.Application.ValueObjects;
using TodoListStart.Application.Interfaces;

namespace TodoListStart.Application.Models
{
    public class TodoItem : TodoItemValue, IDateTimeAudit, IEntityIdentity
    {
        public TodoList TodoList { get; set; }
    }
}
