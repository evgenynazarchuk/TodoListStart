using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoListStart.Application.Models;
using TodoListStart.Application.ValueObjects;

namespace TodoListStart.Application.Services.Repository
{
    public partial class Repository
    {
        public async Task<bool> IsExistItemTitle(TodoItemValue todoItem)
        {
            var existItem = await _dbContext.Set<TodoItem>()
                .AsNoTracking()
                .AnyAsync(i => i.TodoListId == todoItem.TodoListId && i.Title == todoItem.Title);
            return existItem;
        }
    }
}
