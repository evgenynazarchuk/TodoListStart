using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoListStart.Application.Models;
using TodoListStart.Application.ValueObjects;

namespace TodoListStart.Application.Services.Repository
{
    public partial class Repository
    {
        public async Task<bool> IsExistItemTitleInList(TodoItemValue todoItem)
        {
            var existItem = await Read<TodoItem>()
                .AnyAsync(i => i.TodoListId == todoItem.TodoListId && i.Title == todoItem.Title);
            return existItem;
        }
    }
}
