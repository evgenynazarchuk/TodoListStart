using TodoListStart.Application.Models;
using TodoListStart.Application.ValueObjects;
using TodoListStart.IntegrationTests.Support.Builder;

namespace TodoListStart.IntegrationTests.Support.Data
{
    public partial class DataHelper
    {
        public Note AddNote(NoteValue noteValue = null, int? listNoteId = null)
        {
            listNoteId ??= AddListNote().Id;
            noteValue ??= NoteValueBuilder
                .CreateDefaultBuilder()
                .Configure(i =>
                {
                    i.ListNoteId = listNoteId.Value;
                })
                .Build();
            var note = _mapper.Map<NoteValue, Note>(noteValue);
            _repo.Add(note).GetAwaiter().GetResult();
            return note;
        }
    }
}
