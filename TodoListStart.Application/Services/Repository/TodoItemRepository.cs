﻿using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoListStart.Application.Models;
using TodoListStart.Application.ValueObjects;

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
    }
}
