namespace TodoListStart.Application.Constants
{
    public static class ErrorMessages
    {
        public const string InternalServerError = "Internal Server Error.";
        public const string NotFound = "Not Found.";
        public const string NullObject = "Object is null.";

        public const string TitleListErrors = "TodoListTitleErrors";
        public const string TitleItemErrors = "TodoItemTitleErrors";
        public const string ListGroupErrors = "todo-list-errors";
        public const string ItemGroupErrors = "todo-item-errors";

        public const string ItemTitleEmpty = "The field Item Title must be filled";
        public const string ItemNotUnique = "The todo item must be unique in todo list.";

        public const string ListTitleEmpty = "The field List Title must be filled";
        public const string ListNotExist = "The field Item TodoListId must be exist.";
        public const string ListNotUnique = "The todo list must be unique in todo list.";
    }
}
