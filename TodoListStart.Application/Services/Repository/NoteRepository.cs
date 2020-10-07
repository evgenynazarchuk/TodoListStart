using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoListStart.Application.Models;
using TodoListStart.Application.ValueObjects;
using System.Collections.Generic;
using System.Linq;

namespace TodoListStart.Application.Services.Repository
{
    public partial class Repository
    {
        public async Task<bool> IsExistNoteTextInListNote(NoteValue noteValue)
        {
            var existItem = await Read<Note>()
                .AnyAsync(
                i => i.ListNoteId == noteValue.ListNoteId 
                && noteValue.Id != i.Id 
                && i.Text == noteValue.Text
                );
            return existItem;
        }
        public async Task<List<Note>> GetAllPublicNotes()
        {
            return await Read<Note>().Where(notes => notes.IsPublic == true).ToListAsync();
        }
    }
}
