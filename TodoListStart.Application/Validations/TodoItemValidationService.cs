using System.Collections.Generic;
using System.Threading.Tasks;
using TodoListStart.Application.Services;
using TodoListStart.Application.ValueObjects;
using TodoListStart.Application.Models;
using Microsoft.EntityFrameworkCore;
using TodoListStart.Application.Constants;
using TodoListStart.Application.Controllers;

namespace TodoListStart.Application.Validations
{
    public class TodoItemValidationService
    {
        private readonly Repository _repo;
        public TodoItemValidationService(Repository repo)
        {
            _repo = repo;
        }
        public async Task<ModelErrorResult> Validate(TodoItemValue todoItem)
        {
            var errors = new ModelErrorResult();
            
            // todoList должен существовать
            var todoIsExist = _repo.Exist<TodoList>(todoItem.TodoListId);

            if (!todoIsExist)
            {
                errors.Errors.Add("valid-1", new List<string>() { Errors.TodoListNotExist });
            }

            // имя айтема уникально в рамках одного списка
            var itemIsExist = await _repo.GetDbContext<TodoItem>()
                .AsNoTracking()
                .AnyAsync(item => item.TodoListId == todoItem.TodoListId && item.Title == todoItem.Title);

            if (itemIsExist)
            {
                errors.Errors.Add("valid-2", new List<string>() { Errors.TodoItemNotUnique });
            }
            
            //
            return errors;
        }
    }
}
