using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoListStart.Application.Models;
using TodoListStart.Application.ValueObjects;

namespace TodoListStart.Application.Interfaces
{
    public interface IRepository
    {
        Task<List<TModel>> ReadAsync<TModel>()
            where TModel : class, new();
        Task<TModel> AddAsync<TModel>(TModel entity)
            where TModel : class, new();
        Task<TModel> FindAsync<TModel>(int id)
            where TModel : class, new(); 
        Task UpdateAsync<TModel>(TModel entity)
            where TModel : class, new();
        Task RemoveAsync<TModel>(TModel entity)
            where TModel : class, new();
        Task<IEnumerable<TodoItem>> GetTodoItemsByTodoListId(int id);
        Task<bool> IsExist<TModel>(int id)
            where TModel : class, IEntityIdentity, new();
        Task<bool> IsExistItemTitle(TodoItemValue todoItem);
        Task<bool> IsExistTitleList(TodoListValue todoList);
    }
}
