using System;
using TodoListStart.Application.Interfaces;
using System.ComponentModel.DataAnnotations;
using TodoListStart.Application.Constants;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoListStart.Application.ValueObjects
{
    public class NoteValue : IEntityIdentity, IDateTimeAudit
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? DueDate { get; set; }
        public int ListNoteId { get; set; }
        public bool IsPublic { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
