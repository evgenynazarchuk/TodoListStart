using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoListStart.Application.Interfaces
{
    public interface IAuthAudit
    {
        string CreatedBy { get; set; }
        string ModifiedBy { get; set; }
    }
}
