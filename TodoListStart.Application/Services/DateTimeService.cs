using System;
using TodoListStart.Application.Interfaces;

namespace TodoListStart.Application.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime Now => DateTime.Now;
    }
}
