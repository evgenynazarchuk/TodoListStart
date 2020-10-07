using System.Collections.Generic;
using System.Threading.Tasks;
using TodoListStart.Application.ValueObjects;
using TodoListStart.Application.Models;
using TodoListStart.Application.Constants;
using System;

namespace TodoListStart.Application.Services.Validation
{
    public partial class ValidationService
    {
        public virtual async Task<List<string>> ValidateNote(NoteValue noteValue, string method)
        {
            var errorMessages = new List<string>();

            if (IsNull(noteValue))
            {
                errorMessages.Add(ErrorMessages.NullObject);
                return errorMessages;
            }

            if (IsNullOrEmptyOrWhiteSpace(noteValue.Text))
            {
                errorMessages.Add(ErrorMessages.NoteEmpty);
            }

            if (noteValue.Text.Length > 144)
            {
                errorMessages.Add(ErrorMessages.NoteTextIncorrectLenght);
            }

            if (noteValue.DueDate != null && noteValue.DueDate < DateTime.Today)
            {
                errorMessages.Add(ErrorMessages.NoteDueDateLessCurrentDate);
            }

            if (!await _repo.IsExist<ListNote>(noteValue.ListNoteId))
            {
                errorMessages.Add(ErrorMessages.ListNoteNotExist);
            }

            if (await _repo.IsExistNoteTextInListNote(noteValue))
            {
                errorMessages.Add(ErrorMessages.NoteNotUnique);
            }

            return errorMessages;
        }
    }
}
