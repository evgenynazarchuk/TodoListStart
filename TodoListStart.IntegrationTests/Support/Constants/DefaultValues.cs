using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text;

namespace TodoListStart.IntegrationTests.Support.Constants
{
    public class DefaultValues
    {
        public static string NoteText = "Text";
        public static bool NoteIsCompleted = false;
        public static bool NoteIsPublic = false;
        public static int NoteListNoteId = 1;

        public static string ListNoteTitle = "Title";

        public static DateTime? NoteDueDate => null;
        public static DateTime NoteExpiredDueDate => DateTime.Today - TimeSpan.FromDays(1);
    }
}
