using System;
using TodoListStart.Application.ValueObjects;
using TodoListStart.IntegrationTests.Support.Constants;

namespace TodoListStart.IntegrationTests.Support.Builder
{
    public class ListNoteValueBuilder
    {
        private readonly ListNoteValue _listNoteValue;
        private ListNoteValueBuilder()
        {
            _listNoteValue = new ListNoteValue();
        }
        private ListNoteValueBuilder(ListNoteValue listNote)
        {
            _listNoteValue = listNote;
        }
        public static ListNoteValueBuilder CreateDefaultBuilder()
        {
            var todoList = new ListNoteValue()
            {
                Title = DefaultValues.ListNoteTitle
            };
            return new ListNoteValueBuilder(todoList);
        }
        public ListNoteValueBuilder Configure(Action<ListNoteValue> config)
        {
            config(_listNoteValue);
            return this;
        }
        public ListNoteValue Build()
        {
            return _listNoteValue;
        }
    }
}
