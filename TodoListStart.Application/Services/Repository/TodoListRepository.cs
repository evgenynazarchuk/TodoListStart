using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoListStart.Application.Models;
using TodoListStart.Application.ValueObjects;

namespace TodoListStart.Application.Services.Repository
{
    public partial class Repository
    {
        public virtual async Task<IEnumerable<TodoItem>> GetTodoItemsByTodoListId(int id)
        {
            var items = await _dbContext.TodoItems
                .Where(item => item.TodoListId == id)
                .ToListAsync();
            return items;
        }
        public virtual async Task<bool> IsExistTitleList(TodoListValue todoList)
        {
            var result = await _dbContext
                .Set<TodoList>()
                .AnyAsync(l => l.Title == todoList.Title && todoList.Id != l.Id);
            return result;
        }
    }
}
