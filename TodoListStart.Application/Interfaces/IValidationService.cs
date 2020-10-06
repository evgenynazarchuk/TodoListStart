using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoListStart.Application.Controllers;
using TodoListStart.Application.ValueObjects;

namespace TodoListStart.Application.Interfaces
{
    public interface IValidationService
    {
        Task<List<string>> ValidateTodoList(TodoListValue todoList);
        Task<List<string>> ValidationTodoItem(TodoItemValue todoItem);
    }
}
