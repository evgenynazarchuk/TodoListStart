using System;
using TodoListStart.Application.Interfaces;

namespace TodoListStart.Application.ApplicationServices
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime Now => DateTime.Now;
    }
}
