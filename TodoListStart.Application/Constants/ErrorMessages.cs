namespace TodoListStart.Application.Constants
{
    public static class ErrorMessages
    {
        public const string InternalServerError = "Internal Server Error.";
        public const string NotFound = "Not Found.";
        public const string NullObject = "Object is null.";

        public const string NoteEmpty = "The Note must be filled.";
        public const string NoteNotUnique = "The Note must be unique in current list notes.";
        public const string NoteTextIncorrectLenght = "The Note Text must be less than 144 characters.";
        public const string NoteDueDateLessCurrentDate = "The Note Due Date must be more than current date";

        public const string ListNoteEmpty = "The Title must be filled.";
        public const string ListNoteNotExist = "The List Note must be exist.";
        public const string ListNoteTitleNotUnique = "The List Note must be unique in list notes.";
        public const string ListNoteTitleIncorrectLenght = "The List Note Title must be less than 144 characters.";
    }
}
