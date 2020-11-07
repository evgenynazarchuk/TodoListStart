namespace TodoListStart.Application.Constants
{
    public static class ErrorMessages
    {
        public const string InternalServerError = "Internal Server Error.";
        public const string NotFound = "Not Found.";
        public const string NullObject = "Object is null.";

        public const string NoteIsEmpty = "The Note must be filled.";
        public const string NoteIsNotUnique = "The Note must be unique in current list notes.";
        public const string NoteTextIsIncorrectLenght = "The Note Text must be less than 144 characters.";
        public const string NoteDueDateIsLessCurrentDate = "The Note Due Date must be more than current date";

        public const string ListNoteIsEmpty = "The Title must be filled.";
        public const string ListNoteIsNotExist = "The List Note must be exist.";
        public const string ListNoteTitleIsNotUnique = "The List Note must be unique in list notes.";
        public const string ListNoteTitleIsIncorrectLenght = "The List Note Title must be less than 144 characters.";
    }
}
