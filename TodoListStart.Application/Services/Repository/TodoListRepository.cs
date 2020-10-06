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
            var items = await Read<TodoItem>()
                .Where(item => item.TodoListId == id)
                .ToListAsync();
            return items;
        }
        public virtual async Task<bool> IsExistListTitle(TodoListValue todoList)
        {
            var result = await Read<TodoList>()
                .AnyAsync(l => l.Title == todoList.Title && todoList.Id != l.Id);
            return result;
        }
    }
}
