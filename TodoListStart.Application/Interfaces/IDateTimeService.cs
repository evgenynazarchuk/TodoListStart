using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoListStart.Application.Interfaces
{
    public interface IDateTimeService
    {
        public DateTime Now { get; }
    }
}
