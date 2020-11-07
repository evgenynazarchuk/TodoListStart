using System.Collections.Generic;
using System.Threading.Tasks;
using TodoListStart.Application.ValueObjects;
using TodoListStart.Application.Models;
using TodoListStart.Application.Constants;
using System;
using System.Linq;

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
                errorMessages.Add(ErrorMessages.NoteIsEmpty);
            }

            if (noteValue.Text.Length > 144)
            {
                errorMessages.Add(ErrorMessages.NoteTextIsIncorrectLenght);
            }

            if (noteValue.DueDate != null && noteValue.DueDate < DateTime.Today)
            {
                errorMessages.Add(ErrorMessages.NoteDueDateIsLessCurrentDate);
            }

            if (!await _repo.IsExist<ListNote>(noteValue.ListNoteId))
            {
                errorMessages.Add(ErrorMessages.ListNoteIsNotExist);
            }

            if (await _repo.IsExistNoteTextInListNote(noteValue))
            {
                errorMessages.Add(ErrorMessages.NoteIsNotUnique);
            }

            return errorMessages;
        }
    }
}
