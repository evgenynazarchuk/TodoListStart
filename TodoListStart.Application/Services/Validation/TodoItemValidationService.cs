using System.Collections.Generic;
using System.Threading.Tasks;
using TodoListStart.Application.ValueObjects;
using TodoListStart.Application.Models;
using TodoListStart.Application.Constants;

namespace TodoListStart.Application.Services.Validation
{
    public partial class ValidationService
    {
        public virtual async Task<List<string>> ValidateTodoItem(TodoItemValue todoItem, string method)
        {
            var errorMessages = new List<string>();

            if (IsNull(todoItem))
            {
                errorMessages.Add(ErrorMessages.NullObject);
                return errorMessages;
            }

            if (IsNullOrEmptyOrWhiteSpace(todoItem.Title))
            {
                errorMessages.Add(ErrorMessages.ItemTitleEmpty);
            }

            var todoIsExist = await _repo.IsExist<TodoList>(todoItem.TodoListId);
            if (!todoIsExist)
            {
                errorMessages.Add(ErrorMessages.ListNotExist);
            }

            if (await _repo.IsExistItemTitleInList(todoItem))
            {
                errorMessages.Add(ErrorMessages.ItemNotUnique);
            }

            return errorMessages;
        }
    }
}
