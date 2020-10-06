using System.Collections.Generic;
using TodoListStart.Application.ValueObjects;
using TodoListStart.IntegrationTests.Support.Constants;

namespace TodoListStart.IntegrationTests.Support.Facade
{
    public partial class FacadeHelper
    {
        public RequestResult<List<TodoListValue>> GetTodoLists()
        {
            var todoLists = GetRequest<List<TodoListValue>>($"{Urls.TODO_LIST_CONTROLLER}");
            return todoLists;
        }
        public RequestResult<TodoListValue> GetTodoListById(int id)
        {
            var todoList = GetRequest<TodoListValue>($"{Urls.TODO_LIST_CONTROLLER}/{id}");
            return todoList;
        }
        public RequestResult<TodoListValue> PostTodoList(TodoListValue todoListValue)
        {
            var response = PostRequest($"{Urls.TODO_LIST_CONTROLLER}", todoListValue);
            return response;
        }
        public RequestResult<bool> PutTodoList(TodoListValue todoListValue)
        {
            var response = PutRequest($"{Urls.TODO_LIST_CONTROLLER}", todoListValue);
            return response;
        }
        public RequestResult<bool> DeleteTodoList(int id)
        {
            var response = DeleteRequest($"{Urls.TODO_LIST_CONTROLLER}/{id}");
            return response;
        }
        public RequestResult<List<TodoItemValue>> GetItemsByListId(int id)
        {
            var todoItems = GetRequest<List<TodoItemValue>>($"{Urls.TODO_LIST_CONTROLLER}/{id}/items");
            return todoItems;
        }
    }
}
