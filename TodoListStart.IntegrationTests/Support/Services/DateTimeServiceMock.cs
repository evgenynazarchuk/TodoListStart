using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using TodoListStart.Application.Interfaces;

namespace TodoListStart.IntegrationTests.Support.Services
{
    public class DateTimeServiceMock : IDateTimeService
    {
        public DateTime _dateTime { get; private set; }
        public DateTime Now { get => _dateTime; }
        public DateTimeServiceMock()
        {
            _dateTime = DateTime.Now;
        }
        public void SetCurrentDateTime(int year, int month, int day, int hour, int minute, int second)
        {
            _dateTime = new DateTime(year, month, day, hour, minute, second);
        }
        public void SetCurrentDateTime(DateTime dateTime)
        {
            _dateTime = dateTime;
        }
    }
}
