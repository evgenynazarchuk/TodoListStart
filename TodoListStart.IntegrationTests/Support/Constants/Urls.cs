using System;
using System.Collections.Generic;
using System.Text;

namespace TodoListStart.IntegrationTests.Support.Constants
{
    public static class Urls
    {
        public static string HOST = @"http://localhost/api/v1";
        public static string TODO_ITEM_CONTROLLER = $"{HOST}/todoitem";
        public static string TODO_LIST_CONTROLLER = $"{HOST}/todolist";
    }
}
