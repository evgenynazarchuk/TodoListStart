using TodoListStart.Application.Models;
using TodoListStart.Application.ValueObjects;
using TodoListStart.IntegrationTests.Support.Builder;

namespace TodoListStart.IntegrationTests.Support.Data
{
    public partial class DataHelper
    {
        public ListNote AddTodoList(ListNoteValue listNoteValue = null)
        {
            listNoteValue ??= ListNoteValueBuilder
                .CreateDefaultBuilder()
                .Build();
            var listNote = _mapper.Map<ListNoteValue, ListNote>(listNoteValue);
            _repo.Add(listNote).GetAwaiter().GetResult();
            return listNote;
        }
    }
}
