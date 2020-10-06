using System.Collections.Generic;
using System.Threading.Tasks;
using TodoListStart.Application.ApplicationServices;
using TodoListStart.Application.ValueObjects;
using TodoListStart.Application.Models;
using Microsoft.EntityFrameworkCore;
using TodoListStart.Application.Constants;
using TodoListStart.Application.Controllers;
using TodoListStart.Application.Interfaces;

namespace TodoListStart.Application.Services.Validation
{
    public partial class ValidationService
    {
        public virtual async Task<List<string>> ValidateTodoList(TodoListValue todoList)
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

            if (await _repo.IsExistTitleList(todoList))
            {
                errorMessages.Add(ErrorMessages.ListNotUnique);
            }

            return errorMessages;
        }
    }
}
