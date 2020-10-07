using System.Collections.Generic;
using TodoListStart.Application.ValueObjects;
using TodoListStart.Application.Interfaces;

namespace TodoListStart.Application.Models
{
    public class ListNote : ListNoteValue, IDateTimeAudit, IEntityIdentity
    {
        public HashSet<Note> Notes { get; set; }
    }
}
