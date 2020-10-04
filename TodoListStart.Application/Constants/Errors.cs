using System;
using System.Collections.Generic;
using System.Text;

namespace TodoListStart.Application.Constants
{
    public static class Errors
    {
        public const string ErrorTitle = "One or more validation errors occurred.";
        public const string NotFoundError = "Not Found";
        public const string ItemTitleEmpty = "The field Item Title must be filled";
        public const string ItemTitleLength = "The field Item Title must be; a string with a minimum length of 1 and a maximum length of 140.";
        public const string ListTitleEmpty = "The field List Title must be filled";
        public const string ListTitleLength = "The field Item List must be filled";

    }
}
