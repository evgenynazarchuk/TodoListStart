using System.Collections.Generic;
using TodoListStart.Application.ValueObjects;
using TodoListStart.IntegrationTests.Support.Constants;

namespace TodoListStart.IntegrationTests.Support.Facade
{
    public partial class FacadeHelper
    {
        public RequestResult<List<ListNoteValue>> GetTodoLists()
        {
            var todoLists = GetRequest<List<ListNoteValue>>($"{Urls.LISTNOTE_CONTROLLER}");
            return todoLists;
        }
        public RequestResult<ListNoteValue> GetTodoListById(int id)
        {
            var todoList = GetRequest<ListNoteValue>($"{Urls.LISTNOTE_CONTROLLER}/{id}");
            return todoList;
        }
        public RequestResult<ListNoteValue> PostTodoList(ListNoteValue todoListValue)
        {
            var response = PostRequest($"{Urls.LISTNOTE_CONTROLLER}", todoListValue);
            return response;
        }
        public RequestResult<bool> PutTodoList(ListNoteValue todoListValue)
        {
            var response = PutRequest($"{Urls.LISTNOTE_CONTROLLER}", todoListValue);
            return response;
        }
        public RequestResult<bool> DeleteTodoList(int id)
        {
            var response = DeleteRequest($"{Urls.LISTNOTE_CONTROLLER}/{id}");
            return response;
        }
        public RequestResult<List<NoteValue>> GetItemsByListId(int id)
        {
            var todoItems = GetRequest<List<NoteValue>>($"{Urls.LISTNOTE_CONTROLLER}/{id}/items");
            return todoItems;
        }
    }
}
