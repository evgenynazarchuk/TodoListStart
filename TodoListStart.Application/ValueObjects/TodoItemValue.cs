using System;
using TodoListStart.Application.Interfaces;
using System.ComponentModel.DataAnnotations;
using TodoListStart.Application.Constants;

namespace TodoListStart.Application.ValueObjects
{
    public class TodoItemValue : IEntityIdentity, IDateTimeAudit
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [StringLength(maximumLength: 140)]
        public string Body { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? DueDate { get; set; }
        [Required]
        public int TodoListId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
