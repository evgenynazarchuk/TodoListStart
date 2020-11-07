using TodoListStart.Application.Models;
using TodoListStart.Application.ValueObjects;
using TodoListStart.IntegrationTests.Support.Builder;
using Microsoft.Extensions.DependencyInjection;
using TodoListStart.Application.Interfaces;

namespace TodoListStart.IntegrationTests.Support.Data
{
    public partial class DataHelper
    {
        public Note AddNote(NoteValue noteValue = null, int? listNoteId = null)
        {
            listNoteId ??= AddListNote().Id;
            noteValue ??= NoteValueBuilder.Build();
            var note = _mapper.Map<NoteValue, Note>(noteValue);

            _repo = _services.GetRequiredService<IRepository>();
            _repo.Add(note).GetAwaiter().GetResult();
            return note;
        }
    }
}
