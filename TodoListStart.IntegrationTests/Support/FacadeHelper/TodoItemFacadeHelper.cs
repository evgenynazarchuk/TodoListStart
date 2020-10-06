using System.Collections.Generic;
using TodoListStart.Application.ValueObjects;
using TodoListStart.IntegrationTests.Support.Constants;

namespace TodoListStart.IntegrationTests.Support.Facade
{
    public partial class FacadeHelper
    {
        public RequestResult<List<TodoItemValue>> GetTodoItems()
        {
            var todoItems = GetRequest<List<TodoItemValue>>($"{Urls.TODO_ITEM_CONTROLLER}");
            return todoItems;
        }
        public RequestResult<TodoItemValue> GetTodoItemById(int id)
        {
            var todoItem = GetRequest<TodoItemValue>($"{Urls.TODO_ITEM_CONTROLLER}/{id}");
            return todoItem;
        }
        public RequestResult<TodoItemValue> PostTodoItem(TodoItemValue todoItemValue)
        {
            var response = PostRequest($"{Urls.TODO_ITEM_CONTROLLER}", todoItemValue);
            return response;
        }
        public RequestResult<bool> PutTodoItem(TodoItemValue todoItemValue)
        {
            var response = PutRequest($"{Urls.TODO_ITEM_CONTROLLER}", todoItemValue);
            return response;
        }
        public RequestResult<bool> DeleteTodoItem(int id)
        {
            var response = DeleteRequest($"{Urls.TODO_ITEM_CONTROLLER}/{id}");
            return response;
        }
    }
}
