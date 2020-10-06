using System;
using System.ComponentModel.DataAnnotations;
using TodoListStart.Application.Constants;
using TodoListStart.Application.Interfaces;

namespace TodoListStart.Application.ValueObjects
{
    public class TodoListValue : IEntityIdentity, IDateTimeAudit
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
