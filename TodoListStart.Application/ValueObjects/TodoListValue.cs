using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TodoListStart.Application.Constants;
using TodoListStart.Application.Interfaces;

namespace TodoListStart.Application.ValueObjects
{
    public class TodoListValue : IEntityIdentity, IDateTimeAudit
    {
        public int Id { get; set; }
        [Required(ErrorMessage = Errors.ListTitleEmpty)]
        [StringLength(maximumLength: 140, MinimumLength = 1, ErrorMessage = Errors.ListTitleLength)]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
