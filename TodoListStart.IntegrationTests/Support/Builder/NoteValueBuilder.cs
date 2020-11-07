using System;
using TodoListStart.Application.Models;
using TodoListStart.Application.ValueObjects;
using TodoListStart.IntegrationTests.Support.Constants;

namespace TodoListStart.IntegrationTests.Support.Builder
{
    public class NoteValueBuilder
    {
        public static NoteValue Build()
        {
            var note = new NoteValue()
            {
                Text = DefaultValues.NoteText,
                ListNoteId = DefaultValues.NoteListNoteId,
                IsCompleted = DefaultValues.NoteIsCompleted,
                IsPublic = DefaultValues.NoteIsPublic,
                DueDate = DefaultValues.NoteDueDate
            };
            return note;
        }
    }
}
