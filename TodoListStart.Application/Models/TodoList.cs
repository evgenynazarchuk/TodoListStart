using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoListStart.Application.ValueObjects;
using TodoListStart.Application.Interfaces;

namespace TodoListStart.Application.Models
{
    public class TodoList : TodoListValue, IDateTimeAudit, IEntityIdentity
    {
        public HashSet<TodoItem> TodoItems { get; set; }
    }
}
