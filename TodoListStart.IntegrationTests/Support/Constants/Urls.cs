using System;
using System.Collections.Generic;
using System.Text;

namespace TodoListStart.IntegrationTests.Support.Constants
{
    public static class Urls
    {
        public static string HOST = @"http://localhost:5000/api/v1";
        public static string TODO_LIST_CONTROLLER = $"{Urls.HOST}/todolist";
        public static string TODO_ITEM_CONTROLLER = $"{Urls.HOST}/todoitem";
    }
}
