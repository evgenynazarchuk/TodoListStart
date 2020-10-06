using System.Collections.Generic;
using System.Threading.Tasks;
using TodoListStart.Application.ValueObjects;

namespace TodoListStart.Application.Interfaces
{
    public interface IValidationService
    {
        Task<List<string>> ValidateTodoList(TodoListValue todoList, string httpMethod);
        Task<List<string>> ValidateTodoItem(TodoItemValue todoItem, string httpMethod);
        bool IsNull<TValueObj>(TValueObj entityValue);
        bool IsNullOrEmptyOrWhiteSpace(string entityField);
    }
}
