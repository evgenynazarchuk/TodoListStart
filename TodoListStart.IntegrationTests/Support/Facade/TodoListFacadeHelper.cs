using System.Collections.Generic;
using TodoListStart.Application.ValueObjects;
using TodoListStart.IntegrationTests.Support.Constants;

namespace TodoListStart.IntegrationTests.Support.Facade
{
    public partial class FacadeHelper
    {
        public ResponseResult<List<TodoListValue>> GetTodoLists()
        {
            var todoLists = GetRequest<List<TodoListValue>>($"{Urls.TODO_LIST_CONTROLLER}");
            return todoLists;
        }
        public ResponseResult<TodoListValue> GetTodoListById(int id)
        {
            var todoList = GetRequest<TodoListValue>($"{Urls.TODO_LIST_CONTROLLER}/{id}");
            return todoList;
        }
        public ResponseResult<TodoListValue> PostTodoList(TodoListValue todoListValue)
        {
            var response = PostRequest($"{Urls.TODO_LIST_CONTROLLER}", todoListValue);
            return response;
        }
        public ResponseResult<bool> PutTodoList(TodoListValue todoListValue)
        {
            var response = PutRequest($"{Urls.TODO_LIST_CONTROLLER}", todoListValue);
            return response;
        }
        public ResponseResult<bool> DeleteTodoList(int id)
        {
            var response = DeleteRequest($"{Urls.TODO_LIST_CONTROLLER}/{id}");
            return response;
        }
        public ResponseResult<List<TodoItemValue>> GetItemsByListId(int id)
        {
            var todoItems = GetRequest<List<TodoItemValue>>($"{Urls.TODO_LIST_CONTROLLER}/{id}/items");
            return todoItems;
        }
    }
}
