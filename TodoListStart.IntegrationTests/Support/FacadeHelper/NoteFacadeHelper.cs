using System.Collections.Generic;
using TodoListStart.Application.ValueObjects;
using TodoListStart.IntegrationTests.Support.Constants;

namespace TodoListStart.IntegrationTests.Support.Facade
{
    public partial class FacadeHelper
    {
        public RequestResult<List<NoteValue>> GetNotes()
        {
            var notesValue = GetRequest<List<NoteValue>>($"{Urls.NOTE_CONTROLLER}");
            return notesValue;
        }
        public RequestResult<NoteValue> GetNoteById(int id)
        {
            var noteValue = GetRequest<NoteValue>($"{Urls.NOTE_CONTROLLER}/{id}");
            return noteValue;
        }
        public RequestResult<NoteValue> PostNote(NoteValue noteValue)
        {
            var response = PostRequest($"{Urls.NOTE_CONTROLLER}", noteValue);
            return response;
        }
        public RequestResult<bool> PutNote(NoteValue noteValue)
        {
            var response = PutRequest($"{Urls.NOTE_CONTROLLER}", noteValue);
            return response;
        }
        public RequestResult<bool> DeleteNote(int id)
        {
            var response = DeleteRequest($"{Urls.NOTE_CONTROLLER}/{id}");
            return response;
        }
        public RequestResult<List<NoteValue>> GetAllPublicNotes()
        {
            var notesValue = GetRequest<List<NoteValue>>($"{Urls.NOTE_CONTROLLER}/getallpublic");
            return notesValue;
        }
    }
}
