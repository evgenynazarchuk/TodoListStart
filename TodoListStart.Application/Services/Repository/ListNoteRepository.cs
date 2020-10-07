using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoListStart.Application.Models;
using TodoListStart.Application.ValueObjects;

namespace TodoListStart.Application.Services.Repository
{
    public partial class Repository
    {
        public virtual async Task<IEnumerable<Note>> GetNotesByListNoteId(int id)
        {
            var items = await Read<Note>()
                .Where(item => item.ListNoteId == id)
                .ToListAsync();
            return items;
        }
        public virtual async Task<bool> IsExistListNoteTitle(ListNoteValue todoList)
        {
            var result = await Read<ListNote>()
                .AnyAsync(l => l.Title == todoList.Title && todoList.Id != l.Id);
            return result;
        }
    }
}
