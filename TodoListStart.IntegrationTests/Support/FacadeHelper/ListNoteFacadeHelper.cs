using System.Collections.Generic;
using TodoListStart.Application.ValueObjects;
using TodoListStart.IntegrationTests.Support.Constants;

namespace TodoListStart.IntegrationTests.Support.Facade
{
    public partial class FacadeHelper
    {
        public RequestResult<List<ListNoteValue>> GetListNotes()
        {
            var listNotesValue = GetRequest<List<ListNoteValue>>($"{Urls.LISTNOTE_CONTROLLER}");
            return listNotesValue;
        }
        public RequestResult<ListNoteValue> GetListNoteById(int id)
        {
            var todoList = GetRequest<ListNoteValue>($"{Urls.LISTNOTE_CONTROLLER}/{id}");
            return todoList;
        }
        public RequestResult<ListNoteValue> PostListNote(ListNoteValue listNoteValue)
        {
            var response = PostRequest($"{Urls.LISTNOTE_CONTROLLER}", listNoteValue);
            return response;
        }
        public RequestResult<bool> PutListNote(ListNoteValue listNoteValue)
        {
            var response = PutRequest($"{Urls.LISTNOTE_CONTROLLER}", listNoteValue);
            return response;
        }
        public RequestResult<bool> DeleteListNote(int id)
        {
            var response = DeleteRequest($"{Urls.LISTNOTE_CONTROLLER}/{id}");
            return response;
        }
        public RequestResult<List<NoteValue>> GetNotesByListNoteId(int id)
        {
            var notesValue = GetRequest<List<NoteValue>>($"{Urls.LISTNOTE_CONTROLLER}/{id}/notes");
            return notesValue;
        }
    }
}
