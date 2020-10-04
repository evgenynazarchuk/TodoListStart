using System;

namespace TodoListStart.Application.Interfaces
{
    public interface IDateTimeAudit
    {
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
