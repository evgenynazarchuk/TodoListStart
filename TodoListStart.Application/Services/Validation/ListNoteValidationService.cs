using System.Collections.Generic;
using System.Threading.Tasks;
using TodoListStart.Application.ValueObjects;
using TodoListStart.Application.Constants;

namespace TodoListStart.Application.Services.Validation
{
    public partial class ValidationService
    {
        public virtual async Task<List<string>> ValidateListNote(ListNoteValue listNoteValue, string method)
        {
            var errorMessages = new List<string>();

            if (IsNull(listNoteValue))
            {
                errorMessages.Add(ErrorMessages.NullObject);
                return errorMessages;
            }

            if (IsNullOrEmptyOrWhiteSpace(listNoteValue.Title))
            {
                errorMessages.Add(ErrorMessages.ListNoteIsEmpty);
            }

            if (listNoteValue.Title.Length > 144)
            {
                errorMessages.Add(ErrorMessages.ListNoteTitleIsIncorrectLenght);
            }

            if (await _repo.IsExistListNoteTitle(listNoteValue))
            {
                errorMessages.Add(ErrorMessages.ListNoteTitleIsNotUnique);
            }

            return errorMessages;
        }
    }
}
