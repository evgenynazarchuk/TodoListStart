using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoListStart.Application.Interfaces;

namespace TodoListStart.Application.ValueObjects
{
    public class TodoItemValue : IEntityIdentity, IDateTimeAudit
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int TodoListId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
