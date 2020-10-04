using System;
using System.Collections.Generic;
using System.Text;
using TodoListStart.Application.ValueObjects;
using TodoListStart.Application.Models;
using System.Net.Http;
using System.Text.Json;
using TodoListStart.IntegrationTests.Support.Constants;

namespace TodoListStart.IntegrationTests.Support.Facade
{
    public partial class FacadeHelper
    {
        public ResponseResult<List<TodoItemValue>> GetTodoItems()
        {
            var todoItems = GetRequest<List<TodoItemValue>>($"{Urls.TODO_ITEM_CONTROLLER}");
            return todoItems;
        }
        public ResponseResult<TodoItemValue> GetTodoItemById(int id)
        {
            var todoItem = GetRequest<TodoItemValue>($"{Urls.TODO_ITEM_CONTROLLER}/{id}");
            return todoItem;
        }
        public ResponseResult<TodoItemValue> PostTodoItem(TodoItemValue todoItemValue)
        {
            var response = PostRequest($"{Urls.TODO_ITEM_CONTROLLER}", todoItemValue);
            return response;
        }
        public ResponseResult<bool> PutTodoItem(TodoItemValue todoItemValue)
        {
            var response = PutRequest($"{Urls.TODO_ITEM_CONTROLLER}", todoItemValue);
            return response;
        }
        public ResponseResult<bool> DeleteTodoItem(int id)
        {
            var response = DeleteRequest($"{Urls.TODO_ITEM_CONTROLLER}/{id}");
            return response;
        }
    }
}
