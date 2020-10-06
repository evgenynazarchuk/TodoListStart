using System.Collections.Generic;
using System.Threading.Tasks;
using TodoListStart.Application.ValueObjects;
using TodoListStart.Application.Constants;

namespace TodoListStart.Application.Services.Validation
{
    public partial class ValidationService
    {
        public virtual async Task<List<string>> ValidateTodoList(TodoListValue todoList, string method)
        {
            var errorMessages = new List<string>();

            if (IsNull(todoList))
            {
                errorMessages.Add(ErrorMessages.NullObject);
                return errorMessages;
            }

            if (IsNullOrEmptyOrWhiteSpace(todoList.Title))
            {
                errorMessages.Add(ErrorMessages.ListTitleEmpty);
            }

            if (await _repo.IsExistListTitle(todoList))
            {
                errorMessages.Add(ErrorMessages.ListNotUnique);
            }

            return errorMessages;
        }
    }
}
