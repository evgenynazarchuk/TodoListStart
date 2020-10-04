using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using TodoListStart.Application.ValueObjects;
using TodoListStart.Application.Interfaces;
using TodoListStart.Application.Models;

namespace TodoListStart.Application.Services
{
    public partial class Repository
    {
        public async Task<IEnumerable<TodoItem>> GetTodoItemsByTodoListId(int id)
        {
            var items = await _dbContext.TodoItems.Where(item => item.TodoListId == id).ToListAsync();
            return items;
        }
    }
}
