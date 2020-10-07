using System.Collections.Generic;
using TodoListStart.Application.ValueObjects;
using TodoListStart.IntegrationTests.Support.Constants;

namespace TodoListStart.IntegrationTests.Support.Facade
{
    public partial class FacadeHelper
    {
        public RequestResult<List<NoteValue>> GetTodoItems()
        {
            var todoItems = GetRequest<List<NoteValue>>($"{Urls.NOTE_CONTROLLER}");
            return todoItems;
        }
        public RequestResult<NoteValue> GetTodoItemById(int id)
        {
            var todoItem = GetRequest<NoteValue>($"{Urls.NOTE_CONTROLLER}/{id}");
            return todoItem;
        }
        public RequestResult<NoteValue> PostTodoItem(NoteValue todoItemValue)
        {
            var response = PostRequest($"{Urls.NOTE_CONTROLLER}", todoItemValue);
            return response;
        }
        public RequestResult<bool> PutTodoItem(NoteValue todoItemValue)
        {
            var response = PutRequest($"{Urls.NOTE_CONTROLLER}", todoItemValue);
            return response;
        }
        public RequestResult<bool> DeleteTodoItem(int id)
        {
            var response = DeleteRequest($"{Urls.NOTE_CONTROLLER}/{id}");
            return response;
        }
    }
}
