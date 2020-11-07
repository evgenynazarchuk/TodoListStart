using System;
using TodoListStart.Application.ValueObjects;
using TodoListStart.IntegrationTests.Support.Constants;

namespace TodoListStart.IntegrationTests.Support.Builder
{
    public class ListNoteValueBuilder
    {
        public static ListNoteValue Build()
        {
            var listNote = new ListNoteValue()
            {
                Title = DefaultValues.ListNoteTitle
            };
            return listNote;
        }
    }
}
