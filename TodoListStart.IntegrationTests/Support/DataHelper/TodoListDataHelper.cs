using TodoListStart.Application.Models;
using TodoListStart.Application.ValueObjects;
using TodoListStart.IntegrationTests.Support.Builder;

namespace TodoListStart.IntegrationTests.Support.Data
{
    public partial class DataHelper
    {
        public TodoList AddTodoList(TodoListValue todoListValue = null)
        {
            todoListValue ??= TodoListValueBuilder
                .CreateDefaultBuilder()
                .Build();
            var todoList = _mapper.Map<TodoListValue, TodoList>(todoListValue);
            _repo.Add(todoList).GetAwaiter().GetResult();
            return todoList;
        }
    }
}
