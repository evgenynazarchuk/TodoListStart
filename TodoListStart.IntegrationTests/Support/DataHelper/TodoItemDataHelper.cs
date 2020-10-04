using TodoListStart.Application.Models;
using TodoListStart.Application.ValueObjects;
using TodoListStart.IntegrationTests.Support.Builder;

namespace TodoListStart.IntegrationTests.Support.Data
{
    public partial class DataHelper
    {
        public TodoItem AddTodoItem(TodoItemValue todoItemValue = null, int? todoListId = null)
        {
            todoListId ??= AddTodoList().Id;
            todoItemValue ??= TodoItemValueBuilder
                .CreateDefaultBuilder()
                .Configure(i =>
                {
                    i.TodoListId = todoListId.Value;
                })
                .Build();
            var todoItem = _mapper.Map<TodoItemValue, TodoItem>(todoItemValue);
            _repo.AddAsync(todoItem).GetAwaiter().GetResult();
            return todoItem;
        }
    }
}
