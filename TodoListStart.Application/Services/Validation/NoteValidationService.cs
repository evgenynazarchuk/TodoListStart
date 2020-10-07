using System.Collections.Generic;
using System.Threading.Tasks;
using TodoListStart.Application.ValueObjects;
using TodoListStart.Application.Models;
using TodoListStart.Application.Constants;

namespace TodoListStart.Application.Services.Validation
{
    public partial class ValidationService
    {
        public virtual async Task<List<string>> ValidateNote(NoteValue todoItem, string method)
        {
            var errorMessages = new List<string>();

            if (IsNull(todoItem))
            {
                errorMessages.Add(ErrorMessages.NullObject);
                return errorMessages;
            }

            if (IsNullOrEmptyOrWhiteSpace(todoItem.Text))
            {
                errorMessages.Add(ErrorMessages.NoteEmpty);
            }

            var todoIsExist = await _repo.IsExist<ListNote>(todoItem.ListNoteId);
            if (!todoIsExist)
            {
                errorMessages.Add(ErrorMessages.ListNoteNotExist);
            }

            if (await _repo.IsExistNoteInListNote(todoItem))
            {
                errorMessages.Add(ErrorMessages.NoteNotUnique);
            }

            return errorMessages;
        }
    }
}
