using System;
using TodoListStart.Application.Interfaces;
using TodoListStart.IntegrationTests.Support.Services;

namespace TodoListStart.IntegrationTests.Support
{
    public class DateHelper
    {
        private readonly IDateTimeService _dateTimeService;
        public DateHelper(IDateTimeService dateTimeService)
        {
            _dateTimeService = dateTimeService;
        }
        public void SetCurrentDateTime(int year, int month, int day, int hour = 8, int minute = 0, int second = 1)
        {
            (_dateTimeService as DateTimeServiceMock).SetCurrentDateTime(year, month, day, hour, minute, second);
        }
        public void SetCurrentDateTime(DateTime dateTime)
        {
            (_dateTimeService as DateTimeServiceMock).SetCurrentDateTime(dateTime);
        }
    }
}
