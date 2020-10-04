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
        public List<TodoListValue> GetTodoLists()
        {
            var todoLists = GetRequest<List<TodoListValue>>($"{Urls.TODO_LIST_CONTROLLER}");
            return todoLists;
        }
        public TodoListValue GetTodoListById(int id)
        {
            var todoList = GetRequest<TodoListValue>($"{Urls.TODO_LIST_CONTROLLER}/{id}");
            return todoList;
        }
        public TodoListValue PostTodoList(TodoListValue todoListValue)
        {
            var response = PostRequest($"{Urls.TODO_LIST_CONTROLLER}", todoListValue);
            return response;
        }
        public HttpResponseMessage PutTodoList(TodoListValue todoListValue)
        {
            var response = PutRequest($"{Urls.TODO_LIST_CONTROLLER}", todoListValue);
            return response;
        }
        public HttpResponseMessage DeleteListItem(int id)
        {
            var response = DeleteRequest($"{Urls.TODO_LIST_CONTROLLER}/{id}");
            return response;
        }
        public List<TodoItemValue> GetItemsByListId(int id)
        {
            var todoItems = GetRequest<List<TodoItemValue>>($"{Urls.TODO_LIST_CONTROLLER}/{id}/items");
            return todoItems;
        }
    }
}
