using TodoListStart.Application.ValueObjects;
using TodoListStart.Application.Interfaces;

namespace TodoListStart.Application.Models
{
    public partial class Note : NoteValue, IDateTimeAudit, IEntityIdentity
    {
        public ListNote ListNote { get; set; }
    }
}
