using System;
using TodoListStart.Application.Models;
using TodoListStart.Application.ValueObjects;

namespace TodoListStart.IntegrationTests.Support.Builder
{
    public class NoteValueBuilder
    {
        private readonly NoteValue _noteValue;
        private NoteValueBuilder()
        {
            _noteValue = new Note();
        }
        private NoteValueBuilder(NoteValue noteValue)
        {
            _noteValue = noteValue;
        }
        public static NoteValueBuilder CreateDefaultBuilder()
        {
            var todoList = new NoteValue()
            {
                Text = "Text",
                ListNoteId = 1,
                IsCompleted = false,
                IsPublic = false,
                DueDate = null
            };
            return new NoteValueBuilder(todoList);
        }
        public NoteValueBuilder Configure(Action<NoteValue> config)
        {
            config(_noteValue);
            return this;
        }
        public NoteValue Build()
        {
            return _noteValue;
        }
    }
}
