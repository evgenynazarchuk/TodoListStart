using System;
using TodoListStart.Application.Interfaces;

namespace TodoListStart.Application.ValueObjects
{
    public class ListNoteValue : IEntityIdentity, IDateTimeAudit
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
