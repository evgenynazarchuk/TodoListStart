using System;
using TodoListStart.Application.Interfaces;

namespace TodoListStart.Application.ValueObjects
{
    public class ListNoteValue : IEntityIdentity, IDateTimeAudit, IAuthAudit
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
    }
}
